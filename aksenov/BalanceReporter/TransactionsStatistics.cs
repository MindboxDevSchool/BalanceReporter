namespace BalanceReporter
{
    public class TransactionsStatistics
    {
        public string Account { get; set; }
        
        public double GeneralAmount { get; set; }
        
        public int Month { get; set; }

        public TransactionsStatistics(string account, double amount)
        {
            Account = account;
            GeneralAmount = amount;
        }
        
        public TransactionsStatistics(double amount, int month)
        {
            GeneralAmount = amount;
            Month = month;
        }
    }
}