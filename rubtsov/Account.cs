using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BankAccounts
{
    public class Account
    {
        private readonly IEnumerable<Transaction> _transactions;
        public Account(string filename)
        {
            IDataLoader dataLoader = new CsvDataLoader(filename);
            _transactions = dataLoader.LoadTransactions;
        }

        public IEnumerable<Transaction> MonthlyTransactions(int year, int month)
        {
            return _transactions
                    .Where(tr => tr.Date.Year == year && tr.Date.Month == month)
                    .OrderBy(tr => tr.Date);
        }
        
        public IEnumerable<Transaction> YearlyTransactions(int year)
        {
            return _transactions
                    .Where(tr => tr.Date.Year == year)
                    .OrderBy(tr => tr.Date);
        }

        public double AverageIncome(DateTime from, DateTime to)
        {
            return _transactions
                    .Where(tr => tr.Sum > 0 && tr.Date >= from && tr.Date <= to)
                    .Average(tr => tr.Sum);
        }
        
        public double AverageExpenses(DateTime from, DateTime to)
        {
           return _transactions
                    .Where(tr => tr.Sum < 0 && tr.Date >= from && tr.Date <= to)
                    .Average(tr => tr.Sum);
        }

        public double MaxIncome(DateTime from, DateTime to)
        {
            return _transactions
                    .Where(tr => tr.Sum > 0 && tr.Date >= from && tr.Date <= to) 
                    .Max(tr => tr.Sum);
        }
        
        public double MaxExpenses(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Sum < 0 && tr.Date >= from && tr.Date <= to) 
                .Min(tr => tr.Sum);
        }
        
        public string MaxMoneySender(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Sum > 0 && tr.Date >= from && tr.Date <= to)
                .OrderBy(tr => tr.Sum).Last().CompanyName;
        }
        
        public string MaxMoneyReceiver(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Sum < 0 && tr.Date >= from && tr.Date <= to)
                .OrderBy(tr => tr.Sum).First().CompanyName;
        }
    }
}