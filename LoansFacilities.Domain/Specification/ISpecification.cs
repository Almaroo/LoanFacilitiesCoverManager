using System;
using System.Linq.Expressions;

namespace LoansFacilities.Domain.Specification
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T obj);
        Expression<Func<T, bool>> ToExpression();
    }
}