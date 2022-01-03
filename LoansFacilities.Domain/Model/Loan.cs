using System;
using System.Collections.Generic;
using System.ComponentModel;
using LoansFacilities.Domain.Shared.Enum;

namespace LoansFacilities.Domain.Model
{
    public class Loan
    {
        public int Id { get; }
        public decimal InterestRate { get; }
        public int Amount { get; }
        public decimal Likelihood { get; }
        public UsaState State { get; }
        public int CoveredFacility { get; private set; }
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public Loan(string interestRate, string amount, string id, string likelihood, string state)
        {
            Id = int.Parse(id);
            InterestRate = decimal.Parse(interestRate);
            Amount = int.Parse(amount);
            Likelihood = decimal.Parse(likelihood);
            State = Enum.Parse<UsaState>(state);
        }
        
        public Loan(int id, decimal interestRate, int amount, decimal likelihood, UsaState state)
        {
            Id = id;
            InterestRate = interestRate;
            Amount = amount;
            Likelihood = likelihood;
            State = state;
        }

        internal void CoverFacility(Facility facility)
        {
            CoveredFacility = facility.Id;
        }
    }
}