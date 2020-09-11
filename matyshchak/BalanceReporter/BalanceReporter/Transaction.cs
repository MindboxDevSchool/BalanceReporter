using System;

namespace BalanceReporter
{
    public class Transaction
    {
        public Transaction(DateTime date, string transactionPartner, double amount)
        {
            Date = date;
            TransactionPartner = transactionPartner;
            Amount = amount;
        }

        public DateTime Date { get; }
        public string TransactionPartner { get; }
        public double Amount { get; }
    }
}