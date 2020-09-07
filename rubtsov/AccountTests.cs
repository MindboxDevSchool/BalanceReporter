using System;
using System.IO;
using System.Linq;
using BankAccounts;
using NUnit.Framework;

namespace BankAccountsTests
{
    public class Tests
    {
        private static readonly string FilePath = 
            Directory.GetParent(Environment.CurrentDirectory).Parent?.Parent?.Parent?.FullName + "\\TestData.csv";
        
        #region Debug to CheckPathToFile
        // public TestContext TestContext { get; set; }
        //add in any test method: TestContext.WriteLine(FilePath);
        #endregion
        
        readonly Account _account = new Account(FilePath);
        [Test]
        public void CountMonthlyTransactions_2017_03()
        {
            int expected = 4;
            
            int actual = _account.MonthlyTransactions(2017, 3).Count();
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CountYearlyTransactions_2018()
        {
            int expected = 2;

            int actual = _account.YearlyTransactions(2018).Count();
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAverageIncomeFor_2017_03()
        {
            double expected = 3000;
            
            double actual = _account.AverageIncome(new DateTime(2017, 3, 1), new DateTime(2017, 3, 31));
            
            Assert.AreEqual(expected, actual, 0.001);
        }
        
        [Test]
        public void GetAverageExpensesFor_2017_03()
        {
            double expected = -2500;
            
            double actual = _account.AverageExpenses(new DateTime(2017, 3, 1), new DateTime(2017, 3, 31));
            
            Assert.AreEqual(expected, actual, 0.001);
        }
        
        [Test]
        public void GetMaxIncomeFor_2017_03()
        {
            double expected = 5000;
            
            double actual = _account.MaxIncome(new DateTime(2017, 3, 1), new DateTime(2017,3,31));
            
            Assert.AreEqual(expected, actual, 0.001);
        }
        
        [Test]
        public void GetMaxExpensesFor_2017_03()
        {
            double expected = -3000;
            
            double actual = _account.MaxExpenses(new DateTime(2017, 3, 1), new DateTime(2017,3,31));
            
            Assert.AreEqual(expected, actual, 0.001);
        }
        
        [Test]
        public void GetMaxMoneySenderFor_2017_03()
        {
            string expected = "S7";
            
            string actual = _account.MaxMoneySender(new DateTime(2017, 3, 1), new DateTime(2017,3,31));
            
            Assert.AreEqual(expected, actual);
        }
        
        [Test]
        public void GetMaxMoneyReceiver_2017_03()
        {
            string expected = "HotPoint";
            
            string actual = _account.MaxMoneyReceiver(new DateTime(2017, 3, 1), new DateTime(2017,3,31));
            
            Assert.AreEqual(expected, actual);
        }
    }
}