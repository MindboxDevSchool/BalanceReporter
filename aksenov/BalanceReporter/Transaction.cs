using System;
using System.Collections.Generic;

namespace BalanceReporter
{
    public struct Transaction
    {
        public DateTime Date { get; set; }

        public string Account { get; set; }

        public double Amount { get; set; }

        public Transaction(DateTime date, string account, double amount)
        {
            Date = date;
            Account = account;
            Amount = amount;
        }

        public class Comparer : IEqualityComparer<Transaction>
        {
            public bool Equals(Transaction transaction1, Transaction transaction2)
            {
                return transaction1.Account == transaction2.Account;
            }

            public int GetHashCode(Transaction obj)
            {
                return (int) (obj.Amount % 1024);
            }
        }
    }
}