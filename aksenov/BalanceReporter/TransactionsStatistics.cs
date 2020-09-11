using System;

namespace BalanceReporter
{
    public struct TransactionsStatistics
    {
        public string Account { get; set; }
        
        public double Amount { get; set; }
        
        public int Month { get; set; }

        public TransactionsStatistics(string account, double amount)
        {
            Account = account;
            Amount = amount;
            Month = 1;
        }
        
        public TransactionsStatistics(double amount, int month)
        {
            Amount = amount;
            Month = month;
            Account = String.Empty;
        }
    }
}