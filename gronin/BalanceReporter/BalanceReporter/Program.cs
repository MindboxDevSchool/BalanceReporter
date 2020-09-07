using System;
using FileHelpers;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new FileHelperEngine<Transaction>();
            var transactionsList = engine.ReadFile("myFile0.csv");

            foreach (var x in transactionsList)
            {
                Console.WriteLine(x.Sender);
            }

        }
    }
}