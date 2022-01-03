using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;
using LoansFacilities.Infrastructure.CsvParser.Interface;
using LogParser.Core.Interfaces;

namespace LoansFacilities.Infrastructure.CsvParser
{
    public class CsvCovenantRepository : CsvRepositoryBase, ICovenantRepository
    {
        public IQueryable<Covenant> Covenants { get; set; }
        
        public CsvCovenantRepository(string filePath, ILineParser lineParser) : base(filePath, lineParser) { }

        public Task<IEnumerable<Covenant>> GetCovenants(ISpecification<Covenant> specification)
        {
            Covenants ??= ReadLinesAndParseToCollectionOf<Covenant>().AsQueryable();

            return Task.Factory.StartNew(() =>
            {
                return Covenants
                    .Where(specification.ToExpression())
                    .ToList() as IEnumerable<Covenant>;
            });
        }
    }
}