using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace BalanceReporter
{
    public enum TransactionType
    {
        Income,
        Expense
    }

    public class Balance
    {
        public class Transaction
        {
            [Index(0)] public DateTime Date { get; set; }
            [Index(1)] public string Receiver { get; set; }
            [Index(2)] public double Total { get; set; }

            public Transaction(DateTime date, string receiver, double total)
            {
                Date = date;
                Receiver = receiver;
                Total = total;
            }
        }

        private List<Transaction> _transactions;

        private bool doesTransactionTypeMatch(Transaction transaction, TransactionType transactionType)
        {
            if (transactionType == TransactionType.Income)
            {
                return transaction.Total >= 0;
            }

            return transaction.Total < 0;
        }

        public void SetTransactionsList(List<Transaction> transactions)
        {
            _transactions = transactions;
        }
        
        public void ReadTransactionsFromCsvFile(string filename)
        {
            using (StreamReader streamReader = new StreamReader(filename))
            {
                using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Configuration.Delimiter = ",";
                    csvReader.Configuration.HasHeaderRecord = false;
                    IEnumerable<Transaction> records = csvReader.GetRecords<Transaction>();
                    _transactions = records.ToList();
                }
            }
        }

        public double CalculateTotalForThePeriod(DateTime from, DateTime to, TransactionType transactionType)
        {
            double total = 0;
            foreach (var transaction in _transactions)
            {
                if (doesTransactionTypeMatch(transaction, transactionType)
                    && transaction.Date >= from && transaction.Date <= to)
                {
                    total += transaction.Total;
                }
            }

            return Math.Abs(total);
        }


        public string FindReceiverWithMaxTransactionSums(DateTime from, DateTime to, TransactionType transactionType)
        {
            var receiversIncomes = _transactions
                .Where(transaction => doesTransactionTypeMatch(transaction, transactionType) &&
                                      transaction.Date >= from &&
                                      transaction.Date <= to)
                .GroupBy(transaction => transaction.Receiver)
                .Select(receiver => new
                {
                    Name = receiver.Key,
                    Sum = receiver.Sum(transaction => Math.Abs(transaction.Total))
                });
            if (!receiversIncomes.Any())
            {
                return ""; // todo exception
            }
            var maxIncome = receiversIncomes.Max(x => x.Sum);
            var result = receiversIncomes.First(x => x.Sum == maxIncome);
            return result.Name;
        }
    }
}