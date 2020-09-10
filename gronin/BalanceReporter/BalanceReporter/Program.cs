using System;
using System.IO;
using FileHelpers;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Insert starting date of the period dd-mm-yyyy");
            var from = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Insert ending date of the period dd-mm-yyyy");
            var to = DateTime.Parse(Console.ReadLine());
            
            
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent?.Parent?.FullName + "\\myFile0.csv";
            Accounter account = new Accounter(path);
            Console.WriteLine("AverageIncome: " + account.AverageIncome(from,to));
            Console.WriteLine("AverageExpense: " + account.AverageExpense(from,to));
            Console.WriteLine("MaxIncome: " + account.MaxIncome(from,to));
            Console.WriteLine("MaxExpense: " + account.MaxExpense(from,to));
            Console.WriteLine("MaxMoneySender: " + account.MaxMoneySender(from,to));
            Console.WriteLine("MaxMoneyReceiver: " + account.MaxMoneyReceiver(from,to));
            Console.WriteLine("TotalIncome: " + account.TotalIncome(from,to));
            Console.WriteLine("TotalExpense: " + account.TotalExpense(from,to));
            
        }
    }
}