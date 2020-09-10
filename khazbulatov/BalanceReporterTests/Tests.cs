using NUnit.Framework;
using BalanceReporter;

namespace BalanceReporterTests
{
    [TestFixture]
    public class BalanceReporterTests
    {
        [Test]
        public void LoadTransactions_Loads10000Records()
        {
            // Arrange
            Reporter reporter = new Reporter();
            string filepath = "data/tx00.csv";
            int expectedRecordCount = 10000;

            // Act
            int actualRecordCount = reporter.LoadTransactions(filepath);
            
            // Assert
            Assert.AreEqual(expectedRecordCount, actualRecordCount);
        }
    }
}
