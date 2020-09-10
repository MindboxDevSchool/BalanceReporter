namespace BalanceReporter.model
{
    public class TransactionStats
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string Business { get; set; }
        public bool IsIncoming { get; set; }
        public decimal AmountStats { get; set; }

        public override string ToString()
        {
            return IsIncoming
                ? $"received " + (Business != null ? $"from {Business}" : "") + $"in {Month}/{Year}: {AmountStats}"
                : $"spent    " + (Business != null ? $"on {Business}" : "") + $"in {Month}/{Year}: {AmountStats}";
        }
    }
}
