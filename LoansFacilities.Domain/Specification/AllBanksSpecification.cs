using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class AllBanks : Specification<Bank>
    {
        public override Expression<Func<Bank, bool>> ToExpression()
        {
            return _ => true;
        }
    }
}