using System;
using System.Collections.Generic;
using BalanceReporter.model;

namespace BalanceReporter
{
    public static class Program
    {
        private const string DataDirectoryPath = "data/";
        private static readonly Reporter Reporter = new Reporter();

        public static string InputFilepath(string path)
        {
            Console.Write($"Input a filename: {path}");
            string filename = Console.ReadLine();
            return path + filename;
        }
        
        public static IEnumerable<TransactionStats> GetMonthlyStats(StatsType statsType)
        {
            return Reporter.AggregateAmountByPeriod(statsType.Aggregator);
        }
        
        public static void OutputMonthlyStats(StatsType statsType)
        {
            IEnumerable<TransactionStats> transactionStats = GetMonthlyStats(statsType);
            foreach (TransactionStats stats in transactionStats)
            {
                Console.WriteLine($"{statsType.Label} {stats}");
            }
        }

        public static void LoadData()
        {
            string filepath = InputFilepath(DataDirectoryPath);
            Reporter.LoadTransactions(filepath);
        }

        public static void AggregateData()
        {
            OutputMonthlyStats(StatsType.Total);
            OutputMonthlyStats(StatsType.Average);
            OutputMonthlyStats(StatsType.Maximum);
            OutputMonthlyStats(StatsType.Count);
        }

        public static void Main(string[] args)
        {
            LoadData();
            AggregateData();
        }
    }
}
