using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceReporter.model
{
    public class StatsType
    {
        public static readonly StatsType Total = new StatsType()
        {
            Label = "Total",
            Aggregator = (txGroup) => txGroup.Sum(tx => tx.Amount)
        };
        public static readonly StatsType Average = new StatsType()
        {
            Label = "Average",
            Aggregator = (txGroup) => txGroup.Average(tx => tx.Amount)
        };
        public static readonly StatsType Maximum = new StatsType()
        {
            Label = "Maximum",
            Aggregator = (txGroup) => txGroup.Max(tx => tx.Amount)
        };
        public static readonly StatsType Count = new StatsType()
        {
            Label = "Times",
            Aggregator = (txGroup) => txGroup.Max(tx => tx.Amount)
        };

        public string Label { get; private set; }
        public Func<IEnumerable<Transaction>, decimal> Aggregator { get; private set; }
    }
}
