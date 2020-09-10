using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BalanceReporter.model;

namespace BalanceReporter
{
    public class Reporter
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public IEnumerable<Transaction> Transactions => _transactions;

        public int LoadTransactions(string filepath,
            char separator = ',', bool hasHeader = true)
        {
            using StreamReader reader = new StreamReader(filepath);
            if (hasHeader)
            {
                reader.ReadLine();
            }

            int count = 0;
            List<Transaction> data = new List<Transaction>();
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                data.Add(Transaction.Parse(line, separator));
                ++count;
            }

            _transactions = data;
            return count;
        }

        public IEnumerable<TransactionStats> AggregateAmountByPeriod(
            Func<IEnumerable<Transaction>, decimal> aggregator)
        {
            return Transactions
                .OrderBy(tx => tx.Date)
                .GroupBy(tx => new {tx.Date.Year, tx.Date.Month, tx.IsIncoming})
                .Select(txGroup => new TransactionStats()
                {
                    Year = txGroup.Key.Year,
                    Month = txGroup.Key.Month,
                    IsIncoming = txGroup.Key.IsIncoming,
                    AmountStats = aggregator(txGroup)
                });
        }
        
        // TODO: Duplicate code here, how can I DRY it up?
        public IEnumerable<TransactionStats> AggregateAmountByPeriodAndBusiness(
            Func<IEnumerable<Transaction>, decimal> aggregator)
        {
            return Transactions
                .OrderBy(tx => tx.Date)
                .GroupBy(tx => new {tx.Date.Year, tx.Date.Month, tx.Business, tx.IsIncoming})
                .Select(txGroup => new TransactionStats()
                {
                    Year = txGroup.Key.Year,
                    Month = txGroup.Key.Month,
                    Business = txGroup.Key.Business,
                    IsIncoming = txGroup.Key.IsIncoming,
                    AmountStats = aggregator(txGroup)
                });
        }
    }
}
