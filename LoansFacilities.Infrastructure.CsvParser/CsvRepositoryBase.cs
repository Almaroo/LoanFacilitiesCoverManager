using System.Collections.Generic;
using System.IO;
using LogParser.Core.Interfaces;

namespace LoansFacilities.Infrastructure.CsvParser
{
    public abstract class CsvRepositoryBase
    {
        private string _filePath;
        private readonly ILineParser _lineParser;

        protected CsvRepositoryBase(ILineParser lineParser)
        {
            _lineParser = lineParser;
        }

        protected CsvRepositoryBase(string filePath, ILineParser lineParser)
        {
            _filePath = filePath;
            _lineParser = lineParser;
        }

        private StreamReader GetStreamReader() => new(_filePath);

        protected IEnumerable<T> ReadLinesAndParseToCollectionOf<T>()
        {
            using var loansCsvFileReader = GetStreamReader();
            var result = new List<T>();
            string currentLine;
            var lineCount = 0;

            // skip first line with column names
            // TODO it should be determined by something like isHeaderPresent
            loansCsvFileReader.ReadLine();
            
            while ((currentLine = loansCsvFileReader.ReadLine()) != null)
            {
                if (_lineParser.ValidateLineAndLogResult(++lineCount, currentLine))
                {
                    result.Add(_lineParser.ParseLineTo<T>(currentLine));
                }
            }

            return result;
        }

    }
}