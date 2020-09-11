using System;

namespace BalanceReporterLocal
{
    public class Transaction
    {
        public DateTime Date { get; }
        public string Source { get; }
        public double Sum { get; }

        public Transaction(DateTime date, string source, double sum)
        {
            this.Date = date;
            this.Source = source;
            this.Sum = sum;
        }
    }
}