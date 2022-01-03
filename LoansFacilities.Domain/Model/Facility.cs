using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LoansFacilities.Domain.Model
{
    public class Facility
    {
        public int Id { get; }
        public int BankId { get; }
        public decimal InterestRate { get; }
        public int Amount { get; private set; }
        public List<int> CoveredLoans { get; } = new();

        [EditorBrowsable(EditorBrowsableState.Never)]
        public Facility(string amount, string interestRate, string id, string bankId)
        {
            Amount = int.Parse(amount);
            InterestRate = decimal.Parse(interestRate);
            Id = int.Parse(id);
            BankId = int.Parse(bankId);
        }
        
        public Facility(int id, int bankId, decimal interestRate, int amount)
        {
            Id = id;
            BankId = bankId;
            InterestRate = interestRate;
            Amount = amount;
        }

        internal void CoverWithLoan(Loan loan)
        {
            CoveredLoans.Add(loan.Id);
        }
    }
}