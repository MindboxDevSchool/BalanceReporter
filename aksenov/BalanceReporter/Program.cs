using System;
using System.Collections.Generic;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToCSVfile = String.Empty;

            List<Transaction> transactions = GetTransactionsFromFile(pathToCSVfile);

            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
        }

        public static List<Transaction> GetTransactionsFromFile(string pathToCSVfile)
        {
            throw new NotImplementedException();
        }
    }
}