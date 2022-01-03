using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LoansFacilities.Application.Contracts.Dto;
using LoansFacilities.Application.Contracts.Interface;
using LoansFacilities.Domain.Interface;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Service;
using LoansFacilities.Domain.Shared.Enum;
using LoansFacilities.Domain.Specification;
using LoansFacilities.Infrastructure.CsvParser;
using LoansFacilities.Infrastructure.CsvParser.Interface;
using LogParser.Core.Interfaces;
using LogParser.Core.LineParser;
using LogParser.Core.LineWriter;
using static LogParser.Core.LineParser.LineParserPredefinedRules;

namespace LoansFacilities.Application
{
    public class LoanFacilitiesCalculator : ILoanFacilitiesCalculator
    {
        private ILoanRepository _loanRepository;
        private IBankRepository _bankRepository;
        private IFacilityRepository _facilityRepository;
        private ICovenantRepository _covenantRepository;
        private LoanCoverageManager _coverageManager;
        
        public async Task<List<Loan>> GetLoansAsync() => (await _loanRepository.GetLoans(new AllLoans())).OrderBy(x => x.Id).ToList();
        public async Task<List<Bank>> GetBanksAsync() => (await _bankRepository.GetBanks(new AllBanks())).ToList();
        public async Task<List<Covenant>> GetCovenantsAsync() => (await _covenantRepository.GetCovenants(new AllCovenants())).ToList();
        public async Task<List<Facility>> GetFacilitiesAsync() => (await _facilityRepository.GetFacilities(new AllFacilities())).OrderBy(x => x.Id).ToList();

        private LoanFacilitiesCalculator()
        {
        }
        
        public static ILoanFacilitiesCalculatorBuilder Create() => new Builder();
        
        public class Builder : ILoanFacilitiesCalculatorBuilder
        {
            private readonly LoanFacilitiesCalculator _calculator = new();
            
            public ILoanFacilitiesCalculatorBuilder LoadBanks(ILineParser csvBankLineParser = null)
            {
                csvBankLineParser ??= CsvLineParser
                    .Create()
                    .WithSeparator(',')
                    .WithProperty(
                        "id",
                        IsNotNullOrEmpty(),
                        IsInteger()
                    )
                    .WithProperty(
                        "name",
                        IsNotNullOrEmpty(),
                        MinLength(3),
                        MaxLength(30)
                    )
                    .Build();
            
                var bankFilePath = $@"{Directory.GetCurrentDirectory()}/banks.csv";
                _calculator._bankRepository = new CsvBankRepository(bankFilePath, csvBankLineParser);

                return this;
            }

            public ILoanFacilitiesCalculatorBuilder LoadCovenants(ILineParser csvCovenantLineParser = null)
            {
                csvCovenantLineParser ??= CsvLineParser
                    .Create()
                    .WithSeparator(',')
                    .WithProperty(
                        "facility_id",
                        IsNotNullOrEmpty(),
                        IsInteger()
                    )
                    .WithProperty(
                        "max_default_likelihood",
                        IsFloat(),
                        IsNotNegative()
                    )
                    .WithProperty(
                        "bank_id",
                        IsNotNullOrEmpty(),
                        IsInteger()
                    )
                    .WithProperty(
                        "banned_state",
                        IsNotNullOrEmpty(),
                        (s => Enum.TryParse<UsaState>(s, out _), "Value is not a valid USA state")
                    )
                    .Build();

                var covenantFilePath = $@"{Directory.GetCurrentDirectory()}/covenants.csv";
                _calculator._covenantRepository = new CsvCovenantRepository(covenantFilePath, csvCovenantLineParser);

                return this;
            }

            public ILoanFacilitiesCalculatorBuilder LoadFacilities(ILineParser csvFacilityLineParser = null)
            {
                csvFacilityLineParser ??= CsvLineParser
                    .Create()
                    .WithSeparator(',')
                    .WithProperty(
                        "amount",
                        IsInteger(),
                        IsNotNullOrEmpty()
                    )
                    .WithProperty(
                        "interest_rate",
                        IsFloat(),
                        IsNotNegative(),
                        IsNotNullOrEmpty()
                    )
                    .WithProperty(
                        "id",
                        IsInteger(),
                        IsNotNullOrEmpty()
                    )
                    .WithProperty(
                        "bank_id",
                        IsInteger(),
                        IsNotNullOrEmpty()
                    )
                    .Build();
            
                var facilityFilePath = $@"{Directory.GetCurrentDirectory()}/facilities.csv";
                _calculator._facilityRepository = new CsvFacilityRepository(facilityFilePath, csvFacilityLineParser);

                return this;
            }

            public ILoanFacilitiesCalculatorBuilder LoadLoans(ILineParser csvLoanLineParser = null)
            {
                csvLoanLineParser ??= CsvLineParser
                    .Create()
                    .WithSeparator(',')
                    .WithProperty(
                        "interest_rate",
                        IsNotNullOrEmpty(),
                        IsFloat(),
                        IsNotNegative()
                    )
                    .WithProperty(
                        "amount",
                        IsNotNullOrEmpty(),
                        IsInteger(),
                        IsNotNegative()
                    )
                    .WithProperty(
                        "id",
                        IsNotNullOrEmpty(),
                        IsInteger()
                    )
                    .WithProperty(
                        "default_likelihood",
                        IsNotNullOrEmpty(),
                        IsFloat(),
                        IsNotNegative()
                    )
                    .WithProperty(
                        "state",
                        IsNotNullOrEmpty(),
                        (s => Enum.TryParse<UsaState>(s, out _), "Value is not a valid USA state")
                    )
                    .Build();
            
                var loanFilePath = $@"{Directory.GetCurrentDirectory()}/loans.csv";
                _calculator._loanRepository = new CsvLoanRepository(loanFilePath, csvLoanLineParser);

                return this;
            }

            public ILoanFacilitiesCalculator Build()
            {
                if (_calculator._bankRepository == null)
                    throw new ArgumentException("Make sure to call LoadBanks method before");
                
                if (_calculator._covenantRepository == null)
                    throw new ArgumentException("Make sure to call LoadCovenants method before");
                
                if (_calculator._facilityRepository == null)
                    throw new ArgumentException("Make sure to call LoadFacilities method before");
                
                if (_calculator._loanRepository == null)
                    throw new ArgumentException("Make sure to call LoadLoans method before");
                
                _calculator._coverageManager = new LoanCoverageManager(_calculator._loanRepository, _calculator._covenantRepository);
                return _calculator;
            }
        }
        
        public async Task CoverLoans()
        {
            var loans = await GetLoansAsync();
            var facilities = await GetFacilitiesAsync();

            using var csvLineWriter = new CsvLineWriter($@"{Directory.GetCurrentDirectory()}/assignments.csv");
            csvLineWriter.WriteLine("loan_id", "facility_id");
            
            foreach (var facility in facilities)
            {
                foreach (var loan in loans)
                {
                    if(loan.CoveredFacility != 0)
                        continue;

                    await _coverageManager.Cover(loan, facility);
                    
                    if(loan.CoveredFacility != 0)
                        csvLineWriter.WriteLine(loan.Id.ToString(), loan.CoveredFacility.ToString());
                }
            }
        }
    }
}