using System.Collections.Generic;
using System.Threading.Tasks;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;

namespace LoansFacilities.Domain.Interface
{
    public interface ILoanRepository
    {
        Task<IEnumerable<Loan>> GetLoans(ISpecification<Loan> specification);
    }
}