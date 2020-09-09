using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FileHelpers;

namespace BalanceReporter
{
    public class Accounter
    {
        public readonly IEnumerable<Transaction> _transactions;

        public Accounter(string filename)
        {
            var engine = new FileHelperEngine<Transaction>();
            _transactions = engine.ReadFile(filename);
        }

        public IEnumerable<Transaction> TransactionsPerMonth(int year, int month)
        {
            return _transactions
                .Where(tr => tr.Date.Year == year && tr.Date.Month == month)
                .OrderBy(tr => tr.Date);
        }

        public IEnumerable<Transaction> TransactionsPerYear(int year)
        {
            return _transactions
                .Where(tr => tr.Date.Year == year)
                .OrderBy(tr => tr.Date);
        }


        public double TotalIncome(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount > 0 && tr.Date >= from && tr.Date <= to)
                .Sum(tr => tr.Amount);
        }

        public double TotalExpense(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount < 0 && tr.Date >= from && tr.Date <= to)
                .Sum(tr => tr.Amount);
        }

        public double AverageIncome(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount > 0 && tr.Date >= from && tr.Date <= to)
                .Average(tr => tr.Amount);
        }

        public double AverageExpense(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount < 0 && tr.Date >= from && tr.Date <= to)
                .Average(tr => tr.Amount);
        }

        public double MaxIncome(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount > 0 && tr.Date >= from && tr.Date <= to)
                .Max(tr => tr.Amount);
        }

        public double MaxExpense(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount < 0 && tr.Date >= from && tr.Date <= to)
                .Min(tr => tr.Amount);
        }

        public string MaxMoneySender(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount > 0 && tr.Date >= from && tr.Date <= to)
                .OrderBy(tr => tr.Amount).Last().Sender;
        }

        public string MaxMoneyReceiver(DateTime from, DateTime to)
        {
            return _transactions
                .Where(tr => tr.Amount < 0 && tr.Date >= from && tr.Date <= to)
                .OrderBy(tr => tr.Amount).First().Sender;
        }
    }
}