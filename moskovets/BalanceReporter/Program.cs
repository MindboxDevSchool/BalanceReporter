using System;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Balance balance = new Balance();
            balance.ReadTransactionsFromCsvFile(@"D:\MINDBOX\balance1.txt");
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            Console.WriteLine(balance.CalculateTotalForThePeriod(from, to, TransactionType.Income));
            Console.WriteLine(balance.CalculateTotalForThePeriod(from, to, TransactionType.Expense));
            Console.WriteLine(balance.FindReceiverWithMaxTransactionSums(from, to, TransactionType.Income));
        }
    }
}