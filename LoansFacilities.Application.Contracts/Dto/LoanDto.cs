using LoansFacilities.Domain.Shared.Enum;

namespace LoansFacilities.Application.Contracts.Dto
{
    public class LoanDto
    {
        public int Id { get; init; }
        public decimal InterestRate { get; init; }
        public int Amount { get; init; }
        public decimal Likelihood { get; init; }
        public UsaState State { get; init; }

        public LoanDto() { }
    }
}