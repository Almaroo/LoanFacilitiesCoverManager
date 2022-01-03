using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoansFacilities.Domain.Interface;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;
using LogParser.Core.Interfaces;

namespace LoansFacilities.Infrastructure.CsvParser
{
    public class CsvLoanRepository : CsvRepositoryBase, ILoanRepository
    {
        public IQueryable<Loan> Loans { get; private set; }
        
        public CsvLoanRepository(string filePath, ILineParser lineParser) : base(filePath, lineParser) { }

        public Task<IEnumerable<Loan>> GetLoans(ISpecification<Loan> specification)
        {
            Loans ??= ReadLinesAndParseToCollectionOf<Loan>().AsQueryable();

            return Task.Factory.StartNew(() => Loans
                .Where(specification.ToExpression())
                .ToList() as IEnumerable<Loan>);
        }
    }
}