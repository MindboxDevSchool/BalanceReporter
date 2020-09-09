using System;
using System.IO;
using FileHelpers;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent?.Parent?.FullName + "\\myFile0.csv";
            Accounter account = new Accounter(path);
            // foreach (var x in account._transactions)
            // {
            //     Console.WriteLine(x.Amount);
            // }
            var res = account.TransactionsPerMonth(2019, 3);
            Console.WriteLine("Monthly transactions");
            foreach (var tr in res)
            {
                Console.WriteLine("date: {0} Company: {1}, Sum: {2}", tr.Date, tr.Sender, tr.Amount);
            }
            
            var res2 = account.TransactionsPerYear(2020);
            Console.WriteLine("Yearly transactions");
            foreach (var tr in res2)
            {
                Console.WriteLine("date: {0} Company: {1}, Sum: {2}", tr.Date, tr.Sender, tr.Amount);
            }
            Console.WriteLine(account.AverageIncome(new DateTime(2019, 3, 1), new DateTime(2019,5,31)));
            Console.WriteLine(account.AverageExpense(new DateTime(2019, 3, 1), new DateTime(2019,5,31)));
            Console.WriteLine(account.MaxIncome(new DateTime(2019, 3, 1), new DateTime(2019,5,31)));
            Console.WriteLine(account.MaxExpense(new DateTime(2019, 3, 1), new DateTime(2019,5,31)));
            Console.WriteLine(account.MaxMoneySender(new DateTime(2019, 3, 1), new DateTime(2019,5,31)));
            Console.WriteLine(account.MaxMoneyReceiver(new DateTime(2019, 3, 1), new DateTime(2019,5,31)));
            Console.WriteLine(account.TotalIncome(new DateTime(2021, 1, 1), new DateTime(2021,5,31)));
            Console.WriteLine(account.TotalExpense(new DateTime(2019, 3, 1), new DateTime(2019,5,31)));
            
        }
    }
}