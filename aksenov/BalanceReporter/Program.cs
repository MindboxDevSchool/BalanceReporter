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

            double[] cashFlowForAugust = transactionProcessor.FlowOfFunds(8, 2018);
            Console.WriteLine($"2018, {(Months)(7)}. Income: {cashFlowForAugust[0]}$. Expense: {cashFlowForAugust[1]}$");

            double[] cashFlowFor2020 = transactionProcessor.FlowOfFunds(2020);
            Console.WriteLine($"2020. Income: {cashFlowFor2020[0]}$. Expense: {cashFlowFor2020[1]}$");

            double averageExpense = transactionProcessor.AverageExpense(2020);
            Console.WriteLine($"2020. Average expense: {averageExpense}$ per month.");
            
            double averageIncome = transactionProcessor.AverageIncome(2020);
            Console.WriteLine($"2020. Average income: {averageIncome}$ per month.");

            TransactionsStatistics maxExpenseStatistics = transactionProcessor.MaxExpense(2020);
            Console.WriteLine($"2020. Max expense: {maxExpenseStatistics.Amount}$ in {(Months)(maxExpenseStatistics.Month - 1)}");
            
            TransactionsStatistics maxIncomeStatistics = transactionProcessor.MaxIncome(2020);
            Console.WriteLine($"2020. Max income: {maxIncomeStatistics.Amount}$ in {(Months)(maxIncomeStatistics.Month - 1)}");
            
            TransactionsStatistics profitableAccountStatistics = transactionProcessor.MostProfitableAccount(2020);
            Console.WriteLine($"2020. The most profitable account: {profitableAccountStatistics.Account} ({(profitableAccountStatistics.Amount)}$)");
            
            TransactionsStatistics expensiveAccountMonthStatistics = transactionProcessor.MostExpensiveAccount(5, 2020);
            Console.WriteLine($"2020. The most expensive account in {(Months)(4)}: {expensiveAccountMonthStatistics.Account} ({(expensiveAccountMonthStatistics.Amount)}$)");
            
            TransactionsStatistics expensiveAccountYearStatistics = transactionProcessor.MostExpensiveAccount(2020);
            Console.WriteLine($"2020. The most expensive account: {expensiveAccountYearStatistics.Account} ({(expensiveAccountYearStatistics.Amount)}$)");
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