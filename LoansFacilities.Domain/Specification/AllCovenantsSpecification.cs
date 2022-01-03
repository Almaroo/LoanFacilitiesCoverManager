using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class AllCovenants : Specification<Covenant>
    {
        public override Expression<Func<Covenant, bool>> ToExpression()
        {
            return _ => true;
        }
    }
}