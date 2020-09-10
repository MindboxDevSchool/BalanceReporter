using System;
using System.Collections.Generic;
using BalanceReporter;
using NUnit.Framework;

namespace BalanceReporterTests
{
    public class TransactionsAnalyzerTests
    {
        private List<Transaction> _transactions;
        private TransactionsAnalyzer _transactionsAnalyzer;

        [SetUp]
        public void Setup()
        {
            _transactions = new List<Transaction>
            {
                new Transaction()
                {
                    Date = new DateTime(2020, 1, 1),
                    TransactionPartner = "a",
                    Amount = 1
                },

                new Transaction()
                {
                    Date = new DateTime(2020, 1, 1),
                    TransactionPartner = "b",
                    Amount = 2
                },

                new Transaction()
                {
                    Date = new DateTime(2020, 2, 2),
                    TransactionPartner = "a",
                    Amount = -2
                },
                
                new Transaction()
                {
                    Date = new DateTime(2020, 3, 3),
                    TransactionPartner = "a",
                    Amount = -3
                },
            };
            
            _transactionsAnalyzer = new TransactionsAnalyzer(_transactions);
        }
        
        [Test]
        public void Correctly_calculates_interval_income()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedIncome = 3;

            var actualIncome = _transactionsAnalyzer.IntervalIncome(dateFrom, dateTo);

            Assert.That(actualIncome, Is.EqualTo(expectedIncome));
        }
        
        [Test]
        public void Correctly_calculates_interval_expense()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedExpense = -5;

            var actualIncome = _transactionsAnalyzer.IntervalExpense(dateFrom, dateTo);

            Assert.That(actualIncome, Is.EqualTo(expectedExpense));
        }
        
        [Test]
        public void Correctly_calculates_average_income()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedAverageIncome = 1.5;

            var actualAverageIncome = _transactionsAnalyzer.AverageIncome(dateFrom, dateTo);

            Assert.That(actualAverageIncome, Is.EqualTo(expectedAverageIncome));
        }
        
        [Test]
        public void Correctly_calculates_average_expense()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedAverageExpense = -2.5;

            var actualAverageExpense = _transactionsAnalyzer.AverageExpense(dateFrom, dateTo);

            Assert.That(actualAverageExpense, Is.EqualTo(expectedAverageExpense));
        }
        
        [Test]
        public void Correctly_calculates_max_income()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedMaxIncome = 2;

            var actualMaxIncome = _transactionsAnalyzer.MaxIncome(dateFrom, dateTo);

            Assert.That(actualMaxIncome, Is.EqualTo(expectedMaxIncome));
        }
        
        [Test]
        public void Correctly_calculates_max_expense()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedMaxExpense = -3;

            var actualMaxExpense = _transactionsAnalyzer.MaxExpense(dateFrom, dateTo);

            Assert.That(actualMaxExpense, Is.EqualTo(expectedMaxExpense));
        }
        
        [Test]
        public void Correctly_calculates_max_income_transaction_partner()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedTransactionPartner = "b";

            var actualTransactionPartner = _transactionsAnalyzer.MostIncomeTransactionPartner(dateFrom, dateTo);

            Assert.That(actualTransactionPartner, Is.EqualTo(expectedTransactionPartner));
        }
        
        [Test]
        public void Correctly_calculates_max_expense_transaction_partner()
        {
            var dateFrom = new DateTime(2020, 1, 1);
            var dateTo = new DateTime(2020, 4, 1);
            var expectedTransactionPartner = "a";

            var actualTransactionPartner = _transactionsAnalyzer.MostExpenseTransactionPartner(dateFrom, dateTo);

            Assert.That(actualTransactionPartner, Is.EqualTo(expectedTransactionPartner));
        }
        
    }
}