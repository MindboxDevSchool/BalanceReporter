using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceReporter
{
    public class TransactionProcessor
    {
        private List<Transaction> Transactions { get; set; }

        public TransactionProcessor(List<Transaction> transactions)
        {
            Transactions = transactions;
        }

        public double[] FlowOfFunds(int month, int year)
        {
            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year && t.Date.Month == month).ToList();

            double income = transactionsSample.Where(t => t.Amount > 0).Sum(t => t.Amount);
            double expense = transactionsSample.Where(t => t.Amount < 0).Sum(t => t.Amount);
            
            return new double[] {income, expense};
        }

        public double[] FlowOfFunds(int year)
        {
            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year).ToList();

            double income = transactionsSample.Where(t => t.Amount > 0).Sum(t => t.Amount);
            double expense = transactionsSample.Where(t => t.Amount < 0).Sum(t => t.Amount);
            
            return new double[] {income, expense};
        }

        public double AverageExpense(int year)
        {
            double averageExpense = Transactions.Where(t => t.Date.Year == year && t.Amount < 0).Sum(t => t.Amount);

            averageExpense /= 12.0;
            
            return averageExpense;
        }

        public double AverageIncome(int year)
        {
            double averageIncome = Transactions.Where(t => t.Date.Year == year && t.Amount > 0).Sum(t => t.Amount);

            averageIncome /= 12.0;
            
            return averageIncome;
        }

        public TransactionsStatistics MaxExpense(int year)
        {
            int maxExpenseMonth = 1;
            double maxExpenseAmount = 0;

            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year).ToList();

            for (int month = 1; month <= 12; month++)
            {
                double monthlyAmount = transactionsSample.Where(t => t.Date.Month == month && t.Amount < 0).Sum(t => t.Amount);

                if (monthlyAmount < maxExpenseAmount)
                {
                    maxExpenseAmount = monthlyAmount;
                    maxExpenseMonth = month;
                }
            }
            
            return new TransactionsStatistics(maxExpenseAmount, maxExpenseMonth);
        }
        
        public TransactionsStatistics MaxIncome(int year)
        {
            int maxIncomeMonth = 1;
            double maxIncomeAmount = 0;

            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year).ToList();

            for (int month = 1; month <= 12; month++)
            {
                double monthlyAmount = transactionsSample.Where(t => t.Date.Month == month && t.Amount > 0).Sum(t => t.Amount);

                if (monthlyAmount > maxIncomeAmount)
                {
                    maxIncomeAmount = monthlyAmount;
                    maxIncomeMonth = month;
                }
            }
            
            return new TransactionsStatistics(maxIncomeAmount, maxIncomeMonth);
        }
        
        public TransactionsStatistics MostProfitableAccount(int year)
        {
            string account = String.Empty;
            double amount = 0;
            
            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year && t.Amount > 0).ToList();
            
            List<Transaction> uniqueAccountsTransactions = transactionsSample.Distinct(new Transaction.Comparer()).ToList();

            foreach (var uniqueAccountsTransaction in uniqueAccountsTransactions)
            {
                double temp = transactionsSample.Where(t => t.Account == uniqueAccountsTransaction.Account)
                    .Sum(t => t.Amount);

                if (temp > amount)
                {
                    amount = temp;
                    account = uniqueAccountsTransaction.Account;
                }
            }

            return new TransactionsStatistics(account, amount);
        }
        
        public TransactionsStatistics MostExpensiveAccount(int month, int year)
        {
            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year && t.Date.Month == month && t.Amount < 0).ToList();

            return MostExpensiveAccount(transactionsSample);
        }
        
        public TransactionsStatistics MostExpensiveAccount(int year)
        {
            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year && t.Amount < 0).ToList();

            return MostExpensiveAccount(transactionsSample);
        }

        private TransactionsStatistics MostExpensiveAccount(List<Transaction> transactionsSample)
        {
            string account = String.Empty;
            double amount = 0;
            
            List<Transaction> uniqueAccountsTransactions = transactionsSample.Distinct(new Transaction.Comparer()).ToList();

            foreach (var uniqueAccountsTransaction in uniqueAccountsTransactions)
            {
                double temp = transactionsSample.Where(t => t.Account == uniqueAccountsTransaction.Account)
                    .Sum(t => t.Amount);

                if (temp < amount)
                {
                    amount = temp;
                    account = uniqueAccountsTransaction.Account;
                } 
            }

            return new TransactionsStatistics(account, amount);
        }
    }
}