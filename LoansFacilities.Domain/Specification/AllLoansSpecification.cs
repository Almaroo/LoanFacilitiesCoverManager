using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class AllLoans : Specification<Loan>
    {
        public override Expression<Func<Loan, bool>> ToExpression() => _ => true;
    }
}