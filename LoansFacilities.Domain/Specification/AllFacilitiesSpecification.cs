using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class AllFacilities : Specification<Facility>
    {
        public override Expression<Func<Facility, bool>> ToExpression()
        {
            return _ => true;
        }
    }
}