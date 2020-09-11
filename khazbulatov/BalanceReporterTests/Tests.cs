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
            string filepath = "data/tx00.csv";
            int expectedRecordCount = 10000;

            // Act
            int actualRecordCount = Program.LoadTransactionData(filepath);
            
            // Assert
            Assert.AreEqual(expectedRecordCount, actualRecordCount);
        }
    }
}
