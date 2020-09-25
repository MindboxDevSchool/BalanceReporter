using System.Collections.Generic;
using BalanceReporter;
using BalanceReporter.model;
using NUnit.Framework;

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

        [Test]
        public void AggregateAmountData_MonthlyAverageAmount_Calculates()
        {
            // Arrange
            TransactionGrouper grouper = TransactionGrouper.Monthly;
            TransactionAggregator aggregator = TransactionAggregator.AverageAmount;
            TransactionData[] transactions =
            {
                new TransactionData() {Year = 2000, Month = 01, Amount = 1},
                new TransactionData() {Year = 2000, Month = 01, Amount = 2},
                new TransactionData() {Year = 2000, Month = 01, Amount = 3},
                new TransactionData() {Year = 2000, Month = 02, Amount = 4},
                new TransactionData() {Year = 2000, Month = 02, Amount = 5},
                new TransactionData() {Year = 2000, Month = 02, Amount = 6},
                new TransactionData() {Year = 2000, Month = 03, Amount = 7},
                new TransactionData() {Year = 2000, Month = 03, Amount = 8},
                new TransactionData() {Year = 2000, Month = 03, Amount = 9}
            };
            IEnumerable<TransactionData> expectedData = new TransactionData[]
            {
                new TransactionData() {Year = 2000, Month = 01, Amount = 2},
                new TransactionData() {Year = 2000, Month = 02, Amount = 5},
                new TransactionData() {Year = 2000, Month = 03, Amount = 8}
            };

            // Act
            IEnumerable<TransactionData> actualData = Program.AggregateAmountData(transactions, grouper, aggregator);

            // Assert
            Assert.AreEqual(expectedData, actualData);
        }
        
        [Test]
        public void AggregateAmountData_YearlyTotalAmount_Calculates()
        {
            // Arrange
            TransactionGrouper grouper = TransactionGrouper.Yearly;
            TransactionAggregator aggregator = TransactionAggregator.TotalAmount;
            TransactionData[] transactions =
            {
                new TransactionData() {Year = 2000, Amount = 1},
                new TransactionData() {Year = 2000, Amount = 2},
                new TransactionData() {Year = 2000, Amount = 3},
                new TransactionData() {Year = 2001, Amount = 4},
                new TransactionData() {Year = 2001, Amount = 5},
                new TransactionData() {Year = 2001, Amount = 6},
                new TransactionData() {Year = 2002, Amount = 7},
                new TransactionData() {Year = 2002, Amount = 8},
                new TransactionData() {Year = 2002, Amount = 9}
            };
            IEnumerable<TransactionData> expectedData = new TransactionData[]
            {
                new TransactionData() {Year = 2000, Amount = 6},
                new TransactionData() {Year = 2001, Amount = 15},
                new TransactionData() {Year = 2002, Amount = 24}
            };

            // Act
            IEnumerable<TransactionData> actualData = Program.AggregateAmountData(transactions, grouper, aggregator);

            // Assert
            Assert.AreEqual(expectedData, actualData);
        }
        
        [Test]
        public void AggregateAmountData_OverallMaximumAmount_Calculates()
        {
            // Arrange
            TransactionGrouper grouper = TransactionGrouper.Overall;
            TransactionAggregator aggregator = TransactionAggregator.MaximumAmount;
            TransactionData[] transactions =
            {
                new TransactionData() {Amount = 1},
                new TransactionData() {Amount = 2},
                new TransactionData() {Amount = 3},
                new TransactionData() {Amount = 4},
                new TransactionData() {Amount = 5},
                new TransactionData() {Amount = 6},
                new TransactionData() {Amount = 7},
                new TransactionData() {Amount = 8},
                new TransactionData() {Amount = 9}
            };
            IEnumerable<TransactionData> expectedData = new TransactionData[]
            {
                new TransactionData() {Amount = 9}
            };

            // Act
            IEnumerable<TransactionData> actualData = Program.AggregateAmountData(transactions, grouper, aggregator);

            // Assert
            Assert.AreEqual(expectedData, actualData);
        }
    }
}
