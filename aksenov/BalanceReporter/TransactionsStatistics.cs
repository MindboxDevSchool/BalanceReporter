namespace BalanceReporter
{
    public class TransactionsStatistics
    {
        public string Account { get; set; }
        
        public double Amount { get; set; }
        
        public int Month { get; set; }

        public TransactionsStatistics(string account, double amount)
        {
            Account = account;
            Amount = amount;
        }
        
        public TransactionsStatistics(double amount, int month)
        {
            Amount = amount;
            Month = month;
        }
    }
}