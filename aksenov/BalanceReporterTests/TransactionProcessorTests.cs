using System;
using System.Collections.Generic;
using BalanceReporter;
using NUnit.Framework;

namespace BalanceReporterTests
{
    public class TransactionProcessorTests
    {
        static List<Transaction> transactions = new List<Transaction>()
        {
            new Transaction(new DateTime(2020, 1, 1), "Account6", -2000),
            new Transaction(new DateTime(2020, 1, 1), "Account5", -1500),
            new Transaction(new DateTime(2020, 2, 1), "Account0", -500),
            new Transaction(new DateTime(2020, 3, 1), "Account1", 7000),
            new Transaction(new DateTime(2020, 4, 1), "Account5", -200),
            new Transaction(new DateTime(2020, 5, 1), "Account6", -300),
            new Transaction(new DateTime(2020, 5, 1), "Account2", 200),
            new Transaction(new DateTime(2020, 6, 1), "Account2", 5000),
            new Transaction(new DateTime(2020, 7, 1), "Account0", -4000),
            new Transaction(new DateTime(2020, 8, 1), "Account6", -1000),
            new Transaction(new DateTime(2020, 9, 1), "Account0", -500),
            new Transaction(new DateTime(2020, 10, 1), "Account1", 3000),
            new Transaction(new DateTime(2020, 11, 1), "Account0", -500),
            new Transaction(new DateTime(2020, 12, 1), "Account2", 1000),
            new Transaction(new DateTime(2019, 3, 1), "Account", -500),
            new Transaction(new DateTime(2019, 4, 1), "Account", -7000),
            new Transaction(new DateTime(2019, 5, 1), "Account", 200),
            new Transaction(new DateTime(2018, 6, 1), "Account", 5000),
            new Transaction(new DateTime(2018, 7, 1), "Account", -8000),
            new Transaction(new DateTime(2018, 8, 1), "Account", 5000)
        };
        
        
        [Test]
        public void CashFlowByMonth_TwentyTransactionsInInput()
        {
            //arrange
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            double[] cashFlow = transactionProcessor.FlowOfFunds(1, 2020);
            
            //assert
            Assert.AreEqual(new double[] {0, -3500}, cashFlow);
        }
        
        [Test]
        public void CashFlowByYear_TwentyTransactionsInInput()
        {
            //arrange
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            double[] cashFlow = transactionProcessor.FlowOfFunds(2020);
            
            //assert
            Assert.AreEqual(new double[] {16200, -10500}, cashFlow);
        }
        
        [Test]
        public void AverageExpense_TwentyTransactionsInInput()
        {
            //arrange
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            double averageExpense = transactionProcessor.AverageExpense(2020);
            
            //assert
            Assert.AreEqual(-875, averageExpense);
        }
        
        [Test]
        public void AverageIncome_TwentyTransactionsInInput()
        {
            //arrange
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            double averageExpense = transactionProcessor.AverageIncome(2020);
            
            //assert
            Assert.AreEqual(1350, averageExpense);
        }
        
        [Test]
        public void MaxExpense_TwentyTransactionsInInput()
        {
            //arrange
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            TransactionsStatistics statistics = transactionProcessor.MaxExpense(2020);
            
            //assert
            Assert.AreEqual(-4000, statistics.Amount);
            Assert.AreEqual(7, statistics.Month);
        }
        
        [Test]
        public void MaxIncome_TwentyTransactionsInInput()
        {
            //arrange
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            TransactionsStatistics statistics = transactionProcessor.MaxIncome(2020);
            
            //assert
            Assert.AreEqual(7000, statistics.Amount);
            Assert.AreEqual(3, statistics.Month);
        }
        
        [Test]
        public void MostProfitableAccount_TwentyTransactionsInInput()
        {
            //arrange
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            TransactionsStatistics statistics = transactionProcessor.MostProfitableAccount(2020);
            
            //assert
            Assert.AreEqual("Account1", statistics.Account);
            Assert.AreEqual(10000, statistics.Amount);
        }
        
        [Test]
        public void MostExpensiveAccountByMonth_TwentyTransactionsInInput()
        {
            //arrange
            int year = 2020;
            int month = 1;
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            TransactionsStatistics statistics = transactionProcessor.MostExpensiveAccount(month, year);
            
            //assert
            Assert.AreEqual("Account6", statistics.Account);
            Assert.AreEqual(-2000, statistics.Amount);
        }
        
        [Test]
        public void MostExpensiveAccountByYear_TwentyTransactionsInInput()
        {
            //arrange
            int year = 2020;
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            TransactionsStatistics statistics = transactionProcessor.MostExpensiveAccount(year);
            
            //assert
            Assert.AreEqual("Account0", statistics.Account);
            Assert.AreEqual(-5500, statistics.Amount);
        }
    }
}