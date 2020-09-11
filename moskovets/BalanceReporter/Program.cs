using System;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Balance balance = new Balance();

            Console.WriteLine("Input path to file with csv:");
            string filename = Console.ReadLine();

            Console.WriteLine("Input Dates From and To in format dd/mm/yyyy:");
            Console.Write("From: ");
            DateTime from = DateTime.Parse(Console.ReadLine());
            Console.Write("to: ");
            DateTime to = DateTime.Parse(Console.ReadLine());

            balance.ReadTransactionsFromCsvFile(filename);

            // balance.ReadTransactionsFromCsvFile(@"D:\MINDBOX\balance.txt");
            // DateTime from = new DateTime(2019, 1, 1);
            // DateTime to = new DateTime(2020, 1, 1);

            Console.WriteLine("Total incomes for period: {0}",
                balance.CalculateTotalForThePeriod(from, to, TransactionType.Income));
            Console.WriteLine("Total expenses for period: {0}",
                balance.CalculateTotalForThePeriod(from, to, TransactionType.Expense));
            Console.WriteLine("Receiver with max expenses for period: {0}",
                balance.FindReceiverWithMaxTransactionSums(from, to, TransactionType.Expense));
            Console.WriteLine("Receiver with max incomes for period: {0}",
                balance.FindReceiverWithMaxTransactionSums(from, to, TransactionType.Income));

            Console.WriteLine();
            Console.WriteLine("Report by month");
            Console.WriteLine();
            Reporter.PrintReportTransactionSumsByMonth(balance, from, to);
        }
    }
}