using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private StreamReader GetStreamReader()
        {
            StreamReader streamReader = null;
            
            try
            {
                streamReader = new(_filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to open file: ${_filePath}. Make sure it exists and is not used by other process");
                Console.WriteLine(e.Message);
            }

            return streamReader;
        }

        protected IEnumerable<T> ReadLinesAndParseToCollectionOf<T>()
        {
            using var loansCsvFileReader = GetStreamReader();
            
            // TODO I would rather go with FP approach here with Option<> monad
            if (loansCsvFileReader == null) return Enumerable.Empty<T>();
            
            var result = new List<T>();
            string currentLine;
            var lineCount = 0;

            // skip first line with column names
            // TODO it should be determined by something like isHeaderPresent
            loansCsvFileReader.ReadLine();
            
            while ((currentLine = loansCsvFileReader.ReadLine()) != null)
            {
                // tests each column against set validation rules
                if (_lineParser.ValidateLineAndLogResult(++lineCount, currentLine))
                {
                    // this method makes use of reflection, that is why order in files is crucial
                    result.Add(_lineParser.ParseLineTo<T>(currentLine));
                }
            }

            return result;
        }

    }
}