using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoansFacilities.Domain.Interface;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;
using LoansFacilities.Infrastructure.CsvParser.Interface;
using LogParser.Core.Interfaces;

namespace LoansFacilities.Infrastructure.CsvParser
{
    public class CsvFacilityRepository : CsvRepositoryBase, IFacilityRepository
    {
        public IQueryable<Facility> Facilities { get; private set; }

        public CsvFacilityRepository(string filePath, ILineParser lineParser) : base(filePath, lineParser)
        {
        }

        public Task<IEnumerable<Facility>> GetFacilities(ISpecification<Facility> specification)
        {
            
            Facilities ??= ReadLinesAndParseToCollectionOf<Facility>().AsQueryable();
            
            return Task.Factory.StartNew(() =>
            {
                return Facilities
                    .Where(specification.ToExpression())
                    .ToList() as IEnumerable<Facility>;
            });
        }
    }
}