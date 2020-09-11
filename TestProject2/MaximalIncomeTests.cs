using NUnit.Framework;
using BankTransactions;

namespace TestProject2
{
    public class MaximalIncomeTests
    {
        [SetUp]
        public void Setup()
        {
            MaximalIncomeTest();
            MaximalIncomeTest();
            AverageExpenseTest();
            AverageIncomeTest();
        }
        
        [Test]
        public void MaximalIncomeTest()
        {
            var pays = new[] {5.8, 9.8, 7.6, -1};
            Assert.AreEqual(9.8, BankTransactions.Program.maximal_income(pays));
        }
        
        [Test]
        public void MaximalExpenseTest()
        {
            var pay = new[] {-5.8, -9.8, -7.6, -1, 90.8};
            Assert.AreEqual(-9.8, BankTransactions.Program.maximal_expense(pay));
        }
        
        [Test]
        public void AverageIncomeTest()
        {
            var pay = new[] {-5.8, -9.8, -7.6, -1, 90.8};
            Assert.AreEqual(90.8, BankTransactions.Program.average_income(pay));
        }
        
        [Test]
        public void AverageExpenseTest()
        {
            var pay = new[] {-5.8, -9.8, -7.6, -1, 90.8};
            Assert.AreEqual(-6.05, BankTransactions.Program.average_expense(pay));
        }
        
        [Test]
        public void TopSenderTest()
        {
            var pay = new[] {-5.8, -9.8, -7.6, -1, 90.8};
            var names = new[] {"Maria", "Peter", "Oksana", "Vlad", "Max"};
            Assert.AreEqual("Max", BankTransactions.Program.top_sender(90.8, names,pay));
        }
        
        [Test]
        public void TopRecipientTest()
        {
            var pay = new[] {-5.8, -9.8, -7.6, -1, 90.8};
            var names = new[] {"Maria", "Peter", "Oksana", "Vlad", "Max"};
            Assert.AreEqual("Peter", BankTransactions.Program.top_sender(-9.8, names, pay));
        }
        
        
        
        
        
    }
}