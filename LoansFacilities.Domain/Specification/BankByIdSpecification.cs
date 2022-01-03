using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class BankByIdSpecification : Specification<Bank>
    {
        private readonly int _bankId;

        public BankByIdSpecification(int bankId)
        {
            _bankId = bankId;
        }

        public override Expression<Func<Bank, bool>> ToExpression()
        {
            return bank => bank.Id == _bankId;
        }
    }
}