using System;
using System.Collections;
using System.Linq;
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

        public static void LoadFile()
        {
            string filepath = InputFilepath(DataDirectoryPath);
            Reporter.LoadTransactions(filepath);
        }
        
        public static void OutputMonthlyTotals()
        {
            IEnumerable transactionStats = Reporter.AggregateAmountByPeriod(txGroup =>
                txGroup.Sum(tx => tx.Amount)
            );
            foreach (TransactionStats stats in transactionStats)
            {
                Console.WriteLine($"Total {stats}");
            }
        }
        
        public static void OutputMonthlyMeans()
        {
            IEnumerable transactionStats = Reporter.AggregateAmountByPeriod(txGroup => 
                txGroup.Average(tx => tx.Amount)
            );
            foreach (TransactionStats stats in transactionStats)
            {
                Console.WriteLine($"Average {stats}");
            }
        }
        
        public static void OutputMonthlyMaxima()
        {
            IEnumerable transactionStats = Reporter.AggregateAmountByPeriod(txGroup => 
                txGroup.Max(tx => tx.Amount)
            );
            foreach (TransactionStats stats in transactionStats)
            {
                Console.WriteLine($"Max {stats}");
            }
        }
        
        public static void OutputMonthlyCounts()
        {
            IEnumerable transactionStats = Reporter.AggregateAmountByPeriod(txGroup => 
                txGroup.Count()
            );
            foreach (TransactionStats stats in transactionStats)
            {
                Console.WriteLine($"Times {stats}");
            }
        }

        public static void Main(string[] args)
        {
            LoadFile();
            OutputMonthlyTotals();
            OutputMonthlyMeans();
            OutputMonthlyMaxima();
            OutputMonthlyCounts();
        }
    }
}
