using System;
using System.Collections.Generic;
using System.IO;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToCSVfile = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\csv\\transactions.csv";

            List<Transaction> transactions = GetTransactionsFromFile(pathToCSVfile);
            
            Console.WriteLine(transactions[0].Date + "   " + transactions[0].Account + "   " + transactions[0].Amount);

            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
        }

        public static List<Transaction> GetTransactionsFromFile(string pathToCSVfile)
        {
            List<Transaction> transactions = new List<Transaction>();
            
            using (StreamReader reader = new StreamReader(pathToCSVfile))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine()?.Split(";");

                    if (line != null && line.Length == 3)
                    {
                        string[] dateRow = line[0].Split(".");
                        if (dateRow.Length == 3)
                        {
                            DateTime date = new DateTime(int.Parse(dateRow[2]), int.Parse(dateRow[1]),
                                int.Parse(dateRow[0]));
                            transactions.Add(new Transaction(date, line[1], double.Parse(line[2])));
                        }
                    }
                }
            }

            return transactions;
        }
    }
}