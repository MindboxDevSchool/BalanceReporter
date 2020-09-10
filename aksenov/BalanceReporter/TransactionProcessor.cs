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

        public double[] CashFlowByMonth(int month, int year)
        {
            double expense = 0;
            double income = 0;

            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year && t.Date.Month == month).ToList();

            income = transactionsSample.Where(t => t.Amount > 0).Sum(t => t.Amount);
            expense = transactionsSample.Where(t => t.Amount < 0).Sum(t => t.Amount);
            
            return new double[] {income, expense};
        }

        public double[] CashFlowByYear(int year)
        {
            double expense = 0;
            double income = 0;

            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year).ToList();

            income = transactionsSample.Where(t => t.Amount > 0).Sum(t => t.Amount);
            expense = transactionsSample.Where(t => t.Amount < 0).Sum(t => t.Amount);
            
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
            int month = 1;
            double expense = 0;

            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year).ToList();

            for (int i = 1; i <= 12; i++)
            {
                double temp = transactionsSample.Where(t => t.Date.Month == i && t.Amount < 0).Sum(t => t.Amount);

                if (temp < expense)
                {
                    expense = temp;
                    month = i;
                }
            }
            
            return new TransactionsStatistics(expense, month);
        }
        
        public TransactionsStatistics MaxIncome(int year)
        {
            int month = 1;
            double expense = 0;

            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year).ToList();

            for (int i = 1; i <= 12; i++)
            {
                double temp = transactionsSample.Where(t => t.Date.Month == i && t.Amount > 0).Sum(t => t.Amount);

                if (temp > expense)
                {
                    expense = temp;
                    month = i;
                }
            }
            
            return new TransactionsStatistics(expense, month);
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
        
        public TransactionsStatistics MostExpensiveAccountByMonth(int month, int year)
        {
            string account = String.Empty;
            double amount = 0;
            
            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year && t.Date.Month == month && t.Amount < 0).ToList();
            
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
        
        public TransactionsStatistics MostExpensiveAccountByYear(int year)
        {
            string account = String.Empty;
            double amount = 0;
            
            List<Transaction> transactionsSample = Transactions.Where(t => t.Date.Year == year && t.Amount < 0).ToList();
            
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