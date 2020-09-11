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

        public override bool Equals(object? obj) =>
            (obj is Transaction transaction) && Equals(transaction);

        public bool Equals(Transaction other)
        {
            return Date.Equals(other.Date)
                   && TransactionPartner == other.TransactionPartner
                   && Amount.Equals(other.Amount);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Date, TransactionPartner, Amount);
        }
    }
}