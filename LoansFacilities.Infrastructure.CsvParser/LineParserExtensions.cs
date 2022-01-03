using System;
using LogParser.Core.Interfaces;

namespace LoansFacilities.Infrastructure.CsvParser
{
    public static class LineParserExtensions
    {
        public static T ParseLineTo<T>(this ILineParser parser, string line)
        {
            string[] tokens = line.Split(',');

            if (tokens.Length != parser.GetRules().Count) throw new ArgumentException();

            return (T)Activator.CreateInstance(typeof(T), tokens);
        }
    }
}