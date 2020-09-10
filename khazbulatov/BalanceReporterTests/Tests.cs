using System.Linq;
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
            int actualRecordCount = Program.LoadTransactions(filepath).Count();
            
            // Assert
            Assert.AreEqual(expectedRecordCount, actualRecordCount);
        }
    }
}
