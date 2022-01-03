using System.Collections.Generic;
using System.Threading.Tasks;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;

namespace LoansFacilities.Infrastructure.CsvParser.Interface
{
    public interface ICovenantRepository
    {
        Task<IEnumerable<Covenant>> GetCovenants(ISpecification<Covenant> specification);
    }
}