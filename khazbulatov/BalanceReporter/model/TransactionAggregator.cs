﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceReporter.model
{
    public class TransactionAggregator
    {
        public static readonly TransactionAggregator TotalAmount = new TransactionAggregator()
        {
            Label = "total amount",
            AggregateFunction = (stats, txGroup) =>
            {
                stats.Amount = txGroup.Sum(tx => tx.Amount);
                return stats;
            }
        };

        public static readonly TransactionAggregator AverageAmount = new TransactionAggregator()
        {
            Label = "average amount",
            AggregateFunction = (stats, txGroup) =>
            {
                stats.Amount = txGroup.Average(tx => tx.Amount);
                return stats;
            }
        };

        public static readonly TransactionAggregator MaximumAmount = new TransactionAggregator()
        {
            Label = "maximum amount",
            AggregateFunction = (stats, txGroup) =>
            {
                stats.Amount = txGroup.Max(tx => tx.Amount);
                return stats;
            }
        };

        private string Label { get; set; }
        
        public override string ToString() => Label;
        public Func<TransactionData, IEnumerable<TransactionData>, TransactionData> AggregateFunction
        {
            get;
            private set;
        }
    }
}
