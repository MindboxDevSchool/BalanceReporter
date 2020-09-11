using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceReporter.model
{
    public class TransactionAggregator
    {
        public static readonly TransactionAggregator TotalAmount = new TransactionAggregator()
        {
            Label = "total amount",
            AggregateFunction = (txGroup, stats) =>
            {
                stats.Amount = txGroup.Sum(tx => tx.Amount);
                return stats;
            }
        };
        public static readonly TransactionAggregator AverageAmount = new TransactionAggregator()
        {
            Label = "average amount",
            AggregateFunction = (txGroup, stats) =>
            {
                stats.Amount = txGroup.Average(tx => tx.Amount);
                return stats;
            }
        };
        public static readonly TransactionAggregator MaximumAmount = new TransactionAggregator()
        {
            Label = "maximum amount",
            AggregateFunction = (txGroup, stats) =>
            {
                stats.Amount = txGroup.Max(tx => tx.Amount);
                return stats;
            }
        };

        public string Label
        {
            get;
            private set;
        }
        public Func<IEnumerable<TransactionData>, TransactionData, TransactionData> AggregateFunction
        {
            get;
            private set;
        }
    }
}
