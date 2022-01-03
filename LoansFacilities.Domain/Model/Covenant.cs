using System;
using System.ComponentModel;
using LoansFacilities.Domain.Shared.Enum;

namespace LoansFacilities.Domain.Model
{
    public class Covenant
    {
        public int FacilityId { get; }
        public int BankId { get; }
        public decimal MaxAllowedLikelihood { get; }
        public UsaState BannedState { get; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Covenant(string facilityId, string maxAllowedLikelihood, string bankId, string bannedState)
        {
            FacilityId = int.Parse(facilityId);
            BankId = int.Parse(bankId);
            MaxAllowedLikelihood = decimal.TryParse(maxAllowedLikelihood, out var value) ? value : default;
            BannedState = Enum.Parse<UsaState>(bannedState);
        }
        
        public Covenant(int facilityId, int bankId, decimal maxAllowedLikelihood, UsaState bannedState)
        {
            FacilityId = facilityId;
            BankId = bankId;
            MaxAllowedLikelihood = maxAllowedLikelihood;
            BannedState = bannedState;
        }
    }
}