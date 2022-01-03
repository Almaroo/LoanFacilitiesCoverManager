using LogParser.Core.Interfaces;

namespace LoansFacilities.Application.Contracts.Interface
{
    public interface ILoanFacilitiesCalculatorBuilder
    {
        ILoanFacilitiesCalculatorBuilder LoadBanks(ILineParser csvBankLineParser = null);
        ILoanFacilitiesCalculatorBuilder LoadCovenants(ILineParser csvCovenantLineParser = null);
        ILoanFacilitiesCalculatorBuilder LoadFacilities(ILineParser csvFacilityLineParser = null);
        ILoanFacilitiesCalculatorBuilder LoadLoans(ILineParser csvLoanLineParser = null);

        ILoanFacilitiesCalculator Build();
    }
}