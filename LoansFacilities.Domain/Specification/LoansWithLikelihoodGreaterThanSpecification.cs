using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class LoansWithLikelihoodGreaterThan : Specification<Loan>
    {
        private readonly decimal _likelihoodThreshold;
        
        public LoansWithLikelihoodGreaterThan(decimal likelihoodThreshold)
        {
            _likelihoodThreshold = likelihoodThreshold;
        }
        
        public override Expression<Func<Loan, bool>> ToExpression()
        {
            return loan => loan.Likelihood > _likelihoodThreshold;
        }
    }
}