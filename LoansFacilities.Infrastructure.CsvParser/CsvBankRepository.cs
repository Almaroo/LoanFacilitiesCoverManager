using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoansFacilities.Domain.Interface;
using LoansFacilities.Domain.Model;
using LoansFacilities.Domain.Specification;
using LogParser.Core.Interfaces;

namespace LoansFacilities.Infrastructure.CsvParser
{
    public class CsvBankRepository: CsvRepositoryBase, IBankRepository
    {
        public IQueryable<Bank> Banks { get; set; }
        
        public CsvBankRepository(string filePath, ILineParser lineParser) : base(filePath, lineParser) { }

        public Task<IEnumerable<Bank>> GetBanks(ISpecification<Bank> specification)
        {
            Banks ??= ReadLinesAndParseToCollectionOf<Bank>().AsQueryable();

            return Task.Factory.StartNew(() =>
            {
                return Banks
                    .Where(specification.ToExpression())
                    .ToList() as IEnumerable<Bank>;
            });
        }
    }
}