using LoansFacilities.Domain.Shared.Enum;

namespace LoansFacilities.Application.Contracts.Dto
{
    public class CovenantDto
    {
        public int FacilityId { get; init; }
        public int BankId { get; init; }
        public decimal MaxAllowedLikelihood { get; init; }
        public UsaState State { get; init; }

        public CovenantDto() { }
    }
}