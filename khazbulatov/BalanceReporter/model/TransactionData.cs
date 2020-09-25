using System;

namespace BalanceReporter.model
{
    public class TransactionData : IComparable
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Business { get; set; }
        public bool IsIncoming { get; set; }
        public decimal Amount { get; set; }

        public int CompareTo(object obj)
        {
            TransactionData other = obj as TransactionData;
            if (other == null) throw new ArgumentException();
            return new DateTime(this.Year, this.Month, this.Day)
                .CompareTo(new DateTime(other.Year, other.Month, other.Day));
        }

        public static TransactionData Parse(string str, char separator)
        {
            string[] row = str.Split(separator);
            if (row.Length != 3)
            {
                throw new FormatException();
            }

            DateTime date = DateTime.Parse(row[0]);
            string business = row[1];
            decimal amount = decimal.Parse(row[2]);

            return new TransactionData()
            {
                Year = date.Year,
                Month = date.Month,
                Day = date.Day,
                Business = business,
                IsIncoming = amount > 0,
                Amount = Math.Abs(amount)
            };
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Year, Month, Day, Business, IsIncoming, Amount);
        }

        public override bool Equals(object obj)
        {
            TransactionData other = obj as TransactionData;
            if (other == null) return false;
            return this.Year == other.Year && this.Month == other.Month && this.Day == other.Day
                   && this.IsIncoming == other.IsIncoming && this.Amount == other.Amount
                   && this.Business == other.Business;
        }

        public override string ToString()
        {
            string day = Day != default ? $"{Day}/" : "";
            string month = Month != default ? $"{Month}/" : "";
            string year = Year != default ? $"{Year}" : "";
            string datePrefix =
                Day != default ? " on "
                : Month != default || Year != default ? " in "
                : "";
            return (IsIncoming
                       ? $"received" + (Business != null ? $" from {Business}" : "")
                       : $"spent" + (Business != null ? $" on {Business}" : ""))
                   + $"{datePrefix}{day}{month}{year}: {Amount}";
        }
    }
}
