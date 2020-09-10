using System;

namespace BalanceReporter
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string TransactionPartner { get; set; }
        public double Amount { get; set; }
    }
}