using System;
using System.Collections.Generic;
using System.IO;

namespace BankAccounts
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Debug
            // Account account = new Account(Directory.GetParent(Environment.CurrentDirectory).Parent?.Parent?.FullName + "\\TestData.csv");
            // var res = account.MonthlyTransactions(2017, 3);
            // Console.WriteLine("Monthly transactions");
            // foreach (var tr in res)
            // {
            //     Console.WriteLine("date: {0, 14:d} Company: {1, 10}, Sum: {2, 7}", tr.Date, tr.CompanyName, tr.Sum);
            // }
            //
            // var res2 = account.YearlyTransactions(2016);
            // Console.WriteLine("Yearly transactions");
            // foreach (var tr in res2)
            // {
            //     Console.WriteLine("date: {0, 14:d} Company: {1, 10}, Sum: {2, 7}", tr.Date, tr.CompanyName, tr.Sum);
            // }
            // Console.WriteLine(account.AverageIncome(new DateTime(2017, 3, 1), new DateTime(2017,3,31)));
            // Console.WriteLine(account.AverageExpenses(new DateTime(2017, 3, 1), new DateTime(2017,3,31)));
            // Console.WriteLine(account.MaxIncome(new DateTime(2017, 3, 1), new DateTime(2017,3,31)));
            // Console.WriteLine(account.MaxExpenses(new DateTime(2017, 3, 1), new DateTime(2017,3,31)));
            // Console.WriteLine(account.MaxMoneySender(new DateTime(2017, 3, 1), new DateTime(2017,3,31)));
            // Console.WriteLine(account.MaxMoneyReceiver(new DateTime(2017, 3, 1), new DateTime(2017,3,31)));
            #endregion
            
            Console.WriteLine("Hello World!");
        }
    }
}