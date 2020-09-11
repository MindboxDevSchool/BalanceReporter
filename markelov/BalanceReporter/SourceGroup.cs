namespace BalanceReporter
{
    public class SourceGroup
    {
        public string SourceName { get; set; }
        public decimal SourceSum { get; set; }

        public SourceGroup(string sourceName, decimal sourceSum)
        {
            SourceName = sourceName;
            SourceSum = sourceSum;
        }

        public override string ToString()
        {
            return $"{SourceName} | {SourceSum}";
        }
    }
}