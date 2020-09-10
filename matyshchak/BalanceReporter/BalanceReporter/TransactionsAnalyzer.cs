using System;
using System.Collections.Generic;
using System.Linq;

namespace BalanceReporter
{
    public class TransactionsAnalyzer
    {
        public TransactionsAnalyzer(List<Transaction> transactions)
        {
            Transactions = transactions;
        }

        private List<Transaction> Transactions { get; }

        private IEnumerable<Transaction> IncomeTransactions(DateTime from, DateTime to)
        {
            return Transactions
                .Where(t =>
                    t.Date >= from && t.Date <= to &&
                    t.Amount > 0);
        }
        
        private IEnumerable<Transaction> ExpenseTransactions(DateTime from, DateTime to)
        {
            return Transactions
                .Where(t =>
                    t.Date >= from && t.Date <= to &&
                    t.Amount < 0);
        }
        
        private Transaction MaxIncomeTransaction(DateTime from, DateTime to)
        {
            return IncomeTransactions(from, to)
                .OrderByDescending(t => t.Amount)
                .First();
        }
        
        private Transaction MaxExpenseTransaction(DateTime from, DateTime to)
        {
            return ExpenseTransactions(from, to)
                .OrderBy(t => t.Amount)
                .First();
        }
        
        public double IntervalIncome(DateTime from, DateTime to)
        {
            return IncomeTransactions(from, to).Sum(t => t.Amount);
        }


        public double IntervalExpense(DateTime from, DateTime to)
        {
            return ExpenseTransactions(from, to).Sum(t => t.Amount);
        }
        

        public double AverageIncome(DateTime from, DateTime to)
        {
            return IncomeTransactions(from, to).Average(t => t.Amount);
        }
        

        public double AverageExpense(DateTime from, DateTime to)
        {
           return ExpenseTransactions(from, to).Average(t => t.Amount);
        }

        public double MaxIncome(DateTime from, DateTime to)
        {
            return MaxIncomeTransaction(from, to).Amount;
        }
        
        public double MaxExpense(DateTime from, DateTime to)
        {
            return MaxExpenseTransaction(from, to).Amount;
        }

        public string MostIncomeTransactionPartner(DateTime from, DateTime to)
        {
            return IncomeTransactions(from, to)
                .GroupBy(t => t.TransactionPartner)
                .OrderByDescending(x => x.Sum(t => t.Amount))
                .First().Key;
        }
        
        public string MostExpenseTransactionPartner(DateTime from, DateTime to)
        {
            return ExpenseTransactions(from, to)
                .GroupBy(t => t.TransactionPartner)
                .OrderBy(x => x.Sum(t => t.Amount))
                .First().Key;
        }
    }
}