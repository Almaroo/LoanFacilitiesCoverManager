using System.Threading.Tasks;

namespace LoansFacilities.Application.Contracts.Interface
{
    public interface ILoanFacilitiesCalculator
    {
        Task CoverLoans();
    }
}