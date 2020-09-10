using System;

namespace BalanceReporter.model
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Business { get; set; }
        public decimal Amount { get; set; }

        public static Transaction Parse(string str, char separator = ',')
        {
            string[] row = str.Split(separator);
            
            DateTime date = DateTime.Parse(row[0]);
            string business = row[1];
            decimal amount = decimal.Parse(row[2]);
            
            return new Transaction()
            {
                Date = date,
                Amount = amount,
                Business = business
            };
        }

        public override string ToString()
        {
            string direction = Amount > 0 ? "from" : "to";
            return $"{Math.Abs(Amount)} {direction} {Business} on {Date:d}";
        }
    }
}
