using System;

namespace BalanceReporter.model
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Business { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncoming { get; set; }

        public static Transaction Parse(string str, char separator = ',')
        {
            string[] row = str.Split(separator);

            DateTime date = DateTime.Parse(row[0]);
            string business = row[1];
            decimal amount = decimal.Parse(row[2]);

            return new Transaction()
            {
                Date = date,
                Amount = Math.Abs(amount),
                Business = business,
                IsIncoming = amount > 0,
            };
        }

        public override string ToString()
        {
            return IsIncoming
                ? $"received from {Business} on {Date:d}: {Amount}"
                : $"spent    on   {Business} on {Date:d}: {Amount}";
        }
    }
}
