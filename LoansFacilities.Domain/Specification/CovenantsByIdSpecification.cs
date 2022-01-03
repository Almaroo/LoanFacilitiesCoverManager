using System;
using System.Linq.Expressions;
using LoansFacilities.Domain.Model;

namespace LoansFacilities.Domain.Specification
{
    public class CovenantsByIdSpecification : Specification<Covenant>
    {
        private readonly int _facilityId;

        public CovenantsByIdSpecification(int facilityId)
        {
            _facilityId = facilityId;
        }

        public override Expression<Func<Covenant, bool>> ToExpression()
        {
            return covenant => covenant.FacilityId == _facilityId;
        }
    }
}