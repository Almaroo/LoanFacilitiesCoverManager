using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class FacilityByIdSpecification : Specification<Facility>
    {
        private readonly int _facilityId;

        public FacilityByIdSpecification(int facilityId)
        {
            _facilityId = facilityId;
        }

        public override Expression<Func<Facility, bool>> ToExpression()
        {
            return facility => facility.Id == _facilityId;
        }
    }
}