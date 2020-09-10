using System;
namespace BalanceReporter.Helpers
{
    public class BalanceReporterCsvParser
    {
        public DateTime Date { get; set; }
        public string OrganizationTransaction { get; set; } 
        public double SumTransaction { get; set; }
    }
}