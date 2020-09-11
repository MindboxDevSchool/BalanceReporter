using System;
using System.Collections.Generic;
using BalanceReporter;
using NUnit.Framework;

namespace BalanceReporterTests
{
    public class TransactionsParserFromCsvTests
    {
        [Test]
        public void Parsing_file_in_expected_format_works_fine()
        {
            var directory = TestContext.CurrentContext.TestDirectory;
            var filePath = System.IO.Path.Combine(directory, @"..\..\..\transactions_for_testing.csv");
            var actualTransactions = TransactionsParserFromCsv.Parse(filePath);

            var expectedTransactions = new List<Transaction>()
            {
                new Transaction(
                    new DateTime(2020, 1, 1),
                    "Vkusvill",
                    -1)
            };
            
            Assert.That(actualTransactions, Is.EqualTo(expectedTransactions));
        }
    }
}