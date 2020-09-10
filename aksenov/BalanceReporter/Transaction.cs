using System;

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
    }
}