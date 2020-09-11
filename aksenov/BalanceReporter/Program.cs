using System;
using System.Collections.Generic;
using System.IO;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathToCSVfile = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\csv\\transactions.csv";

            List<Transaction> transactions = GetTransactionsFromFile(pathToCSVfile);
            
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);

            Console.WriteLine("Enter the month and year for which you want to know the transaction statistics:");
            Console.Write("month: ");
            int inputMonth = Convert.ToInt32(Console.ReadLine());
            Console.Write("year: ");
            int inputYear = Convert.ToInt32(Console.ReadLine());

            double[] cashFlowForMonth = transactionProcessor.FlowOfFunds(inputMonth, inputYear);
            Console.WriteLine($"{inputYear}, {(Months)(inputMonth - 1)}. Income: {cashFlowForMonth[0]}$. Expense: {cashFlowForMonth[1]}$");

            double[] cashFlowForYear = transactionProcessor.FlowOfFunds(inputYear);
            Console.WriteLine($"{inputYear}. Income: {cashFlowForYear[0]}$. Expense: {cashFlowForYear[1]}$");

            double averageExpense = transactionProcessor.AverageExpense(inputYear);
            Console.WriteLine($"{inputYear}. Average expense: {averageExpense}$ per month.");
            
            double averageIncome = transactionProcessor.AverageIncome(inputYear);
            Console.WriteLine($"{inputYear}. Average income: {averageIncome}$ per month.");

            TransactionsStatistics maxExpenseStatistics = transactionProcessor.MaxExpense(inputYear);
            Console.WriteLine($"{inputYear}. Max expense: {maxExpenseStatistics.Amount}$ in {(Months)(maxExpenseStatistics.Month - 1)}");
            
            TransactionsStatistics maxIncomeStatistics = transactionProcessor.MaxIncome(inputYear);
            Console.WriteLine($"{inputYear}. Max income: {maxIncomeStatistics.Amount}$ in {(Months)(maxIncomeStatistics.Month - 1)}");
            
            TransactionsStatistics profitableAccountStatistics = transactionProcessor.MostProfitableAccount(inputYear);
            Console.WriteLine($"{inputYear}. The most profitable account: {profitableAccountStatistics.Account} ({(profitableAccountStatistics.Amount)}$)");
            
            TransactionsStatistics expensiveAccountMonthStatistics = transactionProcessor.MostExpensiveAccount(inputMonth, inputYear);
            Console.WriteLine($"{inputYear}. The most expensive account in {(Months)(inputMonth - 1)}: {expensiveAccountMonthStatistics.Account} ({(expensiveAccountMonthStatistics.Amount)}$)");
            
            TransactionsStatistics expensiveAccountYearStatistics = transactionProcessor.MostExpensiveAccount(inputYear);
            Console.WriteLine($"{inputYear}. The most expensive account: {expensiveAccountYearStatistics.Account} ({(expensiveAccountYearStatistics.Amount)}$)");
        }

        enum Months
        {
            January, February, March, April, May, June, July, August, September, October, November, December
        }

        public static List<Transaction> GetTransactionsFromFile(string pathToCSVfile)
        {
            List<Transaction> transactions = new List<Transaction>();
            
            using (StreamReader reader = new StreamReader(pathToCSVfile))
            {
                while (!reader.EndOfStream)
                {
                    string[] line = reader.ReadLine()?.Split(";");

                    if (line != null && line.Length == 3)
                    {
                        string[] dateRow = line[0].Split(".");
                        if (dateRow.Length == 3)
                        {
                            DateTime date = new DateTime(int.Parse(dateRow[2]), int.Parse(dateRow[1]),
                                int.Parse(dateRow[0]));
                            transactions.Add(new Transaction(date, line[1], double.Parse(line[2])));
                        }
                    }
                }
            }

            return transactions;
        }
    }
}