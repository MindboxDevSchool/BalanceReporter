using NUnit.Framework;

namespace BankTransactionsUnitTestsCalculations
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MaximalIncomeTesting()
        {
            var amounts = new[] {-6.7, -7.8, 5, 9.9};
            var maximalIncome = BankingTransactions.maximal_income(amounts);
            Assert.Pass();
        }
    }
}