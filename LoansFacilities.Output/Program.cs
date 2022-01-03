using System;
using System.Diagnostics;
using System.Threading.Tasks;
using LoansFacilities.Application;

namespace LoansFacilities.Output
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Program has started...");
            
            var calculator = LoanFacilitiesCalculator
                .Create()
                .LoadBanks()
                .LoadCovenants()
                .LoadFacilities()
                .LoadLoans()
                .Build();

            Console.WriteLine("Successfully found and opened necessary excel files");

            Console.WriteLine($"Starting covering job at {DateTime.Now:G}");

            var timer = new Stopwatch();
            timer.Start();
            
            // async because in real world scenario calls to repositories would be asynchronous some due to access to db
            await calculator.CoverLoans();
            
            timer.Stop();
            Console.WriteLine($"Task completed at {DateTime.Now:G}, took {timer.Elapsed.Seconds} seconds to complete");
        }
    }
}