using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BalanceReporter.model;

namespace BalanceReporter
{
    public static class Program
    {
        private const string DataDirectoryPath = "data/";
        private static List<TransactionData> Transactions { get; set; }

        public static string InputFilepath(string path)
        {
            Console.Write($"Input a filename: {path}");
            string filename = Console.ReadLine();
            return path + filename;
        }

        public static int LoadTransactionData(string filepath, char separator = ',', bool hasHeader = true)
        {
            using StreamReader reader = new StreamReader(filepath);
            if (hasHeader)
            {
                reader.ReadLine();
            }

            int count = 0;
            List<TransactionData> data = new List<TransactionData>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                data.Add(TransactionData.Parse(line, separator));
                ++count;
            }

            Transactions = data;
            return count;
        }

        public static IEnumerable<TransactionData> AggregateAmountData(IEnumerable<TransactionData> transactionData,
            TransactionGrouper grouper, TransactionAggregator aggregator)
        {
            return transactionData
                .GroupBy(grouper.KeyFunction, aggregator.AggregateFunction);
        }

        public static void OutputTransactions()
        {
            foreach (TransactionData transactionData in Transactions)
            {
                Console.WriteLine($"- {transactionData}");
            }
        }

        public static void OutputTransactionStats(TransactionGrouper grouper, TransactionAggregator aggregator)
        {
            IEnumerable<TransactionData> amountStats = AggregateAmountData(Transactions, grouper, aggregator);
            foreach (TransactionData stats in amountStats)
            {
                Console.WriteLine($"- {grouper} {aggregator} {stats}");
            }
        }

        public static void PrepareData()
        {
            string filepath = InputFilepath(DataDirectoryPath);
            LoadTransactionData(filepath);
            Transactions.Sort();
        }

        public static void OutputStats()
        {
            OutputTransactionStats(TransactionGrouper.Monthly, TransactionAggregator.AverageAmount);
            OutputTransactionStats(TransactionGrouper.Yearly, TransactionAggregator.MaximumAmount);
            OutputTransactionStats(TransactionGrouper.Overall, TransactionAggregator.TotalAmount);
        }

        public static void Main(string[] args)
        {
            PrepareData();
            OutputStats();
        }
    }
}
