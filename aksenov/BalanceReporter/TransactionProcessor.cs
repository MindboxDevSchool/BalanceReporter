using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceReporter
{
    public class TransactionProcessor
    {
        public List<Transaction> Transactions { get; set; }

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
            throw new NotImplementedException();
        }

        public double AverageExpense(int year)
        {
            throw new NotImplementedException();
        }

        public double AverageIncome(int year)
        {
            throw new NotImplementedException();
        }

        public TransactionsStatistics MaxExpense(int year)
        {
            throw new NotImplementedException();
        }
        
        public TransactionsStatistics MaxIncome(int year)
        {
            throw new NotImplementedException();
        }
        
        public TransactionsStatistics MostProfitableAccount(int year)
        {
            throw new NotImplementedException();
        }
        
        public TransactionsStatistics MostExpensiveAccountByMonth(int month)
        {
            throw new NotImplementedException();
        }
        
        public TransactionsStatistics MostExpensiveAccountByYear(int year)
        {
            throw new NotImplementedException();
        }
    }
}