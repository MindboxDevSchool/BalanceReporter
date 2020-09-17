namespace BalanceReporter.Helpers
{
    public class Transaction
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Organization { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            var transactionStr = $"Year: {Year}, ";
            if (Month != default)
                transactionStr += $"Month: {Month}, ";
            if (!string.IsNullOrEmpty(Organization))
                transactionStr += $"Organization: {Organization}, ";
            return transactionStr + $"Value: {Value}";
        }
    }
}