using System;
using System.Collections.Generic;

namespace BalanceReporter
{
    class TransactionProcessor
    {
        public List<Transaction> Transactions { get; set; }

        public TransactionProcessor(List<Transaction> transactions)
        {
            Transactions = transactions;
        }

        public double[] CashFlowByMonth(int month)
        {
            throw new NotImplementedException();
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