using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class Tests
    {
        List<Transaction> exampleTransactions = new List<Transaction>()
        {
            new Transaction(DateTime.Parse("10/05/2015"), "OneWoo", 263),
            new Transaction(DateTime.Parse("10/12/2016"), "FreeRide", 2322),
            new Transaction(DateTime.Parse("11/01/2019"), "GetUp", 1231)
        };
        
        List<Transaction> exampleTwoEqualMaxTransactions = new List<Transaction>()
        {
            new Transaction(DateTime.Parse("10/05/2015"), "OneWoo", -264),
            new Transaction(DateTime.Parse("10/12/2016"), "FreeRide", -2322),
            new Transaction(DateTime.Parse("11/01/2019"), "GetUp", -2322),
            new Transaction(DateTime.Parse("10/12/2016"), "FreeRide", -1522),
            new Transaction(DateTime.Parse("11/01/2016"), "GetUp", -1522)
        };
        
        [Test]
        public void CheckIfFileIsValid_EmptyFile_False()
        {
            string filePath = @"..\..\..\empty.csv";
            bool isFileValid = Program.CheckIfFileIsValid(filePath);
            Assert.AreEqual(false, isFileValid);
        }
        
        [Test]
        public void CheckIfFileIsValid_FileNotFound_False()
        {
            string filePath = @"not_found.csv";
            bool isFileValid = Program.CheckIfFileIsValid(filePath);
            Assert.AreEqual(false, isFileValid);
        }
        
        [Test]
        public void ParseCsvTransactions_BadFormatting_ReturnNull()
        {
            string filePath = @"..\..\..\wrong_format.csv";
            List<Transaction> transactions = Program.ParseCsvTransactions(filePath);
            Assert.IsNull(transactions);
        }
        
        [Test]
        public void GetTransactions_GoodData()
        {
            List<Transaction> transactions = Program.GetTransactions(exampleTransactions, true);
            Assert.AreEqual(1231, transactions[2].TransactionSum);
            Assert.AreEqual(3, transactions.Count);
        }

        [Test]
        public void CalculateTransactionSum_GoodData()
        {
            decimal sum = Program.CalculateTransactionSum(exampleTransactions);
            Assert.AreEqual(3816, sum);
        }

        [Test]
        public void CalculateAverageTransaction_GoodData()
        {
            decimal average = Program.CalculateAverageTransaction(exampleTransactions);
            Assert.AreEqual(1272, average);
        }

        [Test]
        public void GetMaxTransaction_TwoEquallyMaxTransactions()
        {
            List<Transaction> transactions = Program.GetMaxTransaction(exampleTwoEqualMaxTransactions);
            Assert.AreEqual(2, transactions.Count);
            Assert.AreEqual(-2322, transactions[0].TransactionSum);
        }

        [Test]
        public void GetSourceWithMaxTransactionSum_TwoEquallyMaxTransactions()
        {
            List<SourceGroup> sourceGroups = Program.GetSourceWithMaxTransactionSum(exampleTwoEqualMaxTransactions);
            Assert.AreEqual(2, sourceGroups.Count);
            Assert.AreEqual("FreeRide", sourceGroups[0].SourceName);
        }

        [Test]
        public void GroupBalanceDataByYear_GoodData_CorrectlyCountsNumberOfYearlyGroups()
        {
            var transactionsByYears = Program.GroupBalanceDataByYear(exampleTwoEqualMaxTransactions);
            int numberOfYearGroups = 0;
            foreach (var transactionsByYear in transactionsByYears)
            {
                numberOfYearGroups++;
            }
            Assert.AreEqual(3, numberOfYearGroups);
        }
        
        [Test]
        public void GroupBalanceDataByMonth_GoodData_CorrectlyCountsNumberOfMonthlyGroups()
        {
            int numberOfMonthGroups = 0;
            var transactionsByYears = Program.GroupBalanceDataByYear(exampleTwoEqualMaxTransactions);
            foreach (var yearlyGroup in transactionsByYears)
            {
                var transactionsByMonths = Program.GroupBalanceDataByMonth(yearlyGroup);
                foreach (var monthlyGroups in transactionsByMonths)
                {
                    numberOfMonthGroups++;
                }
            }
            Assert.AreEqual(4, numberOfMonthGroups);
        }
    }
}