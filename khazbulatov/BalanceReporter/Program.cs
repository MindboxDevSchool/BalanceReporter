using System;
using System.Collections.Generic;
using System.IO;
using BalanceReporter.model;

namespace BalanceReporter
{
    public static class Program
    {
        private const string DataDirectoryPath = "data/";
        public static string InputFilepath(string path)
        {
            Console.Write($"Input a filename: {path}");
            string filename = Console.ReadLine();
            return path + filename;
        }
        
        public static IEnumerable<Transaction> LoadTransactions(string filename,
            char separator = ',', bool hasHeader = true)
        {
            using StreamReader reader = new StreamReader(filename);
            if (hasHeader)
            {
                reader.ReadLine();
            }
            
            List<Transaction> data = new List<Transaction>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                data.Add(Transaction.Parse(line, separator));
            }
            return data;
        }

        public static void Main(string[] args)
        {
            string filepath = InputFilepath(DataDirectoryPath);
            IEnumerable<Transaction> transactions = LoadTransactions(filepath);
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
