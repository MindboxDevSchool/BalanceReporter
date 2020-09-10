using System;
using System.Collections.Generic;
using BalanceReporter;
using NUnit.Framework;

namespace BalanceReporterTests
{
    public class TransactionProcessorTests
    {
        [Test]
        public void CashFlowByMonth_FiveDifferentTransactionsInInput()
        {
            //arrange
            List<Transaction> transactions = new List<Transaction>()
            {
                new Transaction(new DateTime(2020, 1, 1), "Account", -2000),
                new Transaction(new DateTime(2020, 1, 1), "Account", -3000),
                new Transaction(new DateTime(2020, 1, 1), "Account", 5000),
                new Transaction(new DateTime(2020, 1, 1), "Account", 1000),
                new Transaction(new DateTime(2020, 1, 1), "Account", -500)
            };
            
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            double[] cashFlow = transactionProcessor.CashFlowByMonth(1, 2020);
            
            //assert
            Assert.AreEqual(new double[] {6000, -5500}, cashFlow);
        }
        
        [Test]
        public void CashFlowByMonth_ThreeProfitableTransactionsInInput()
        {
            //arrange
            List<Transaction> transactions = new List<Transaction>()
            {
                new Transaction(new DateTime(2020, 1, 1), "Account", 2000),
                new Transaction(new DateTime(2020, 1, 1), "Account", 3000),
                new Transaction(new DateTime(2020, 1, 1), "Account", 5000),
            };
            
            TransactionProcessor transactionProcessor = new TransactionProcessor(transactions);
            
            //act
            double[] cashFlow = transactionProcessor.CashFlowByMonth(1, 2020);
            
            //assert
            Assert.AreEqual(new double[] {10000, 0}, cashFlow);
        }
    }
}