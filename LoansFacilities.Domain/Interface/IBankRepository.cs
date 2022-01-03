using System.Collections.Generic;
using System.Threading.Tasks;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;

namespace LoansFacilities.Domain.Interface
{
    public interface IBankRepository
    {
        Task<IEnumerable<Bank>> GetBanks(ISpecification<Bank> specification);
    }
}