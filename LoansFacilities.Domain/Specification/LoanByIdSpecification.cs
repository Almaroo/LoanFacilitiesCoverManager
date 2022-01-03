using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class LoanByIdSpecification : Specification<Loan>
    {
        private readonly int _loanId;

        public LoanByIdSpecification(int loanId)
        {
            _loanId = loanId;
        }
        
        public override Expression<Func<Loan, bool>> ToExpression()
        {
            return loan => loan.Id == _loanId;
        }
    }
}