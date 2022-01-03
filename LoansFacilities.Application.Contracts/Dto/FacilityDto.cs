namespace LoansFacilities.Application.Contracts.Dto
{
    public class FacilityDto
    {
        public int Id { get; init; }
        public int BankId { get; init; }
        public decimal InterestRate { get; init; }
        public int Amount { get; init; }

        public FacilityDto() { }
    }
}