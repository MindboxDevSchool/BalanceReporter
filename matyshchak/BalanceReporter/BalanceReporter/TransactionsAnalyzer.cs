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
            return IncomeTransactions(from, to).Max(t => t.Amount);
        }
        

        public double MaxExpense(DateTime from, DateTime to)
        {
            return ExpenseTransactions(from, to).Min(t => t.Amount);
        }
        

        public string BiggestAmountReceivedFrom(DateTime from, DateTime to)
        {
            return IncomeTransactions(from, to)  
                .OrderByDescending(t => t.Amount)
                .First()
                .TransactionPartner;
        }
        

        public string BiggestAmountSentTo(DateTime from, DateTime to)
        {
            return ExpenseTransactions(from, to)  
                .OrderBy(t => t.Amount)
                .First()
                .TransactionPartner;
        }
    }
}