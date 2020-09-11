using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void maximalIncome()
        {
            var pays = new[] {5.6, 7.8, 9.9, -8.1, -6.7};
            var maximalIncome = maximal_income(pays);
            Assert.Pass();
        }
    }
}