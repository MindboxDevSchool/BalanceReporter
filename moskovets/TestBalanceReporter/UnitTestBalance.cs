using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using BalanceReporter;

namespace TestBalanceReporter
{
    public class TestsBalance
    {
        [Test]
        public void TestEmptyTransactionList_CalculateTotalForThePeriod()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Income;

            // act
            var total = balance.CalculateTotalForThePeriod(from, to, transactionType);
            
            // assert
            Assert.AreEqual(0, total);
        }
        
        [Test]
        public void TestEmptyTransactionList_FindReceiverWithMaxTransactionSums()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Income;

            // act
            var total = balance.FindReceiverWithMaxTransactionSums(from, to, transactionType);
            
            // assert
            Assert.AreEqual("", total);
        }
        
        [Test]
        public void TestCorrectTransactionListForIncomes_CalculateTotalForThePeriod()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            transactions.Add(new Balance.Transaction(new DateTime(2019, 2, 1), "a", 10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", -15));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 4, 1), "c", 13));
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Income;

            // act
            var total = balance.CalculateTotalForThePeriod(from, to, transactionType);
            
            // assert
            Assert.AreEqual(23, total);
        }
        
        [Test]
        public void TestCorrectTransactionListForExpenses_CalculateTotalForThePeriod()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            transactions.Add(new Balance.Transaction(new DateTime(2019, 2, 1), "a", 10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", -15));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 4, 1), "c", 13));
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Expense;

            // act
            var total = balance.CalculateTotalForThePeriod(from, to, transactionType);
            
            // assert
            Assert.AreEqual(15, total);
        }
        
        [Test]
        public void TestCorrectTransactionList_CalculateTotalForThePeriod_EmptyResult()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            transactions.Add(new Balance.Transaction(new DateTime(2019, 2, 1), "a", 10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", 15));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 4, 1), "c", 13));
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Expense;

            // act
            var total = balance.CalculateTotalForThePeriod(from, to, transactionType);
            
            // assert
            Assert.AreEqual(0, total);
        }
        
        [Test]
        public void TestCorrectTransactionList_FindReceiverWithMaxTransactionSums_EmptyResult()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            transactions.Add(new Balance.Transaction(new DateTime(2019, 2, 1), "a", 10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", 15));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 4, 1), "c", 13));
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Expense;

            // act
            var receiver = balance.FindReceiverWithMaxTransactionSums(from, to, transactionType);
            
            // assert
            Assert.AreEqual("", receiver);
        }
        
        [Test]
        public void TestCorrectTransactionListForExpenses_FindReceiverWithMaxTransactionSums()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            transactions.Add(new Balance.Transaction(new DateTime(2019, 2, 1), "a", 10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", -10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "c", -15));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", -10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 4, 1), "c", 13));
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Expense;

            // act
            var receiver = balance.FindReceiverWithMaxTransactionSums(from, to, transactionType);
            
            // assert
            Assert.AreEqual("b", receiver);
        }
        
        [Test]
        public void TestCorrectTransactionListForIncomes_FindReceiverWithMaxTransactionSums()
        {
            // arrange 
            List<Balance.Transaction> transactions = new List<Balance.Transaction>();
            transactions.Add(new Balance.Transaction(new DateTime(2019, 2, 1), "a", -10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", 10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "c", 15));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 3, 1), "b", 10));
            transactions.Add(new Balance.Transaction(new DateTime(2019, 4, 1), "c", -13));
            Balance balance = new Balance();
            balance.SetTransactionsList(transactions);
            DateTime from = new DateTime(2019, 1, 1);
            DateTime to = new DateTime(2020, 1, 1);
            TransactionType transactionType = TransactionType.Income;

            // act
            var receiver = balance.FindReceiverWithMaxTransactionSums(from, to, transactionType);
            
            // assert
            Assert.AreEqual("b", receiver);
        }
    }
}