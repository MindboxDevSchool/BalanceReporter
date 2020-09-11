using System;

namespace BalanceReporter
{
    public class Transaction
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionSource { get; set; }
        public decimal TransactionSum { get; set; }

        public Transaction(DateTime transactionDate, string transactionSource, decimal transactionSum)
        {
            TransactionDate = transactionDate;
            TransactionSource = transactionSource;
            TransactionSum = transactionSum;
        }

        public override string ToString()
        {
            return $"{TransactionDate.ToString("MM/dd/yyyy")} " +
                    $"| {TransactionSource} " +
                    $"| {TransactionSum}";
        }
    }
}