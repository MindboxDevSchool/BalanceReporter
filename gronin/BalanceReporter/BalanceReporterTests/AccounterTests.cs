using System;
using System.IO;
using System.Linq;
using BalanceReporter;
using NUnit.Framework;

namespace BalanceReporterTests
{
    public class AccounterTests
    {
        private Accounter account;
        [SetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent?.Parent?.FullName + "\\TestData.csv";
            account = new Accounter(path);
        }

        [Test]
        public void TransactionsPerMonth_return_container_of_correct_length()
        {

            var transactions = account.TransactionsPerMonth(2000, 1);
            int lenght = transactions.Count();
            
            Assert.AreEqual(6,lenght);
        }
        
        [Test]
        public void TransactionsPerYear_return_container_of_correct_length()
        {

            var transactions = account.TransactionsPerYear(2000);
            int lenght = transactions.Count();
            
            Assert.AreEqual(8,lenght);
        }
        
        [Test]
        public void TotalIncome_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var totalIncome = account.TotalIncome(from, to);
            
            Assert.AreEqual(5000,totalIncome);
        }
        
        [Test]
        public void TotalExpense_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var totalExpense = account.TotalExpense(from, to);
            
            Assert.AreEqual(-5000,totalExpense);
        }
        
        [Test]
        public void AverageIncome_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var averageIncome = account.AverageIncome(from, to);
            
            Assert.AreEqual(1667,averageIncome,1);
        }
        
        [Test]
        public void AverageExpense_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var averageExpense = account.AverageExpense(from, to);
            
            Assert.AreEqual(-1667,averageExpense,1);
        }
        
        [Test]
        public void MaxIncome_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var maxIncome = account.MaxIncome(from, to);
            
            Assert.AreEqual(2000,maxIncome);
        }
        
        [Test]
        public void MaxExpense_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var maxExpense = account.MaxExpense(from, to);
            
            Assert.AreEqual(-2000,maxExpense);
        }
        
        [Test]
        public void MaxMoneySender_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var maxMoneySender = account.MaxMoneySender(from, to);
            
            Assert.AreEqual("TWO",maxMoneySender);
        }
        
        [Test]
        public void MaxMoneyReceiver_return_Correct()
        {
            DateTime from = new DateTime(2000,1,1);
            DateTime to = new DateTime(2000,1,5);
            var maxMoneyReceiver = account.MaxMoneyReceiver(from, to);
            
            Assert.AreEqual("ONE",maxMoneyReceiver);
        }
    }
}