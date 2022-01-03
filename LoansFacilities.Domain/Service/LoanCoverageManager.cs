using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoansFacilities.Domain.Interface;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;
using LoansFacilities.Infrastructure.CsvParser.Interface;

namespace LoansFacilities.Domain.Service
{
    public class LoanCoverageManager
    {
        private readonly ICovenantRepository _covenantRepository;
        private readonly ILoanRepository _loanRepository;

        public LoanCoverageManager(
            ILoanRepository loanRepository,
            ICovenantRepository covenantRepository
        )
        {
            _covenantRepository = covenantRepository;
            _loanRepository = loanRepository;
        }

        public async Task Cover(Loan loan, Facility facility)
        {
            var covenants =
                (await _covenantRepository
                    .GetCovenants(new CovenantsByIdSpecification(facility.Id)))
                    .ToList();

            var totalCoveredValue = 0;

            foreach (var coveringLoanId in facility.CoveredLoans)
            {
                var coveringLoan =
                    (await _loanRepository.GetLoans(new LoanByIdSpecification(coveringLoanId)))
                        .FirstOrDefault();

                if (coveringLoan != null)
                {
                    totalCoveredValue += coveringLoan.Amount;
                }
            }

            if (totalCoveredValue + loan.Amount > facility.Amount) return;

            if (!covenants.Any() || ValidateLoan(loan, facility, covenants))
            {
                loan.CoverFacility(facility);
                facility.CoverWithLoan(loan);
                
                // TODO save modified aggregate roots
            }
        }

        private bool ValidateLoan(Loan loan, Facility facility, IEnumerable<Covenant> covenants)
        {
            var covenant = covenants.FirstOrDefault(x => x.BannedState == loan.State);

            if (covenant == null) return true;

            if (covenant.MaxAllowedLikelihood == 0) return false;

            return covenant.MaxAllowedLikelihood > loan.Likelihood;
        }
    }
}