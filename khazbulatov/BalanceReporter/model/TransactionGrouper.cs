using System;

namespace BalanceReporter.model
{
    public class TransactionGrouper
    {
        public static readonly TransactionGrouper Overall = new TransactionGrouper()
        {
            Label = "overall",
            KeyFunction = tx => new TransactionData()
            {
                IsIncoming = tx.IsIncoming
            }
        };

        public static readonly TransactionGrouper Yearly = new TransactionGrouper()
        {
            Label = "yearly",
            KeyFunction = tx => new TransactionData()
            {
                Year = tx.Year,
                IsIncoming = tx.IsIncoming
            }
        };

        public static readonly TransactionGrouper Monthly = new TransactionGrouper()
        {
            Label = "monthly",
            KeyFunction = tx => new TransactionData()
            {
                Year = tx.Year,
                Month = tx.Month,
                IsIncoming = tx.IsIncoming
            }
        };

        public string Label { get; private set; }
        public Func<TransactionData, TransactionData> KeyFunction { get; private set; }
    }
}
