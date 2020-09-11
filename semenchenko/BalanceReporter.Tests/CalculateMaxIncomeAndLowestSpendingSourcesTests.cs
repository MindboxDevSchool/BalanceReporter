using System;
using System.Collections.Generic;
using BalanceReporterLocal;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class CalculateMaxIncomeAndLowestSpendingSourcesTests
    {
        [Test]
        public void ValidData_ReturnsValidResult()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            set.Add(new Transaction(new DateTime(2008, 08, 28), "Alex", 100));
            set.Add(new Transaction(new DateTime(2008, 08, 16), "Sonya", -200));
            set.Add(new Transaction(new DateTime(2010, 08, 28), "Alex", 300));
            set.Add(new Transaction(new DateTime(2010, 09, 28), "Mary", -50));
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            string[] sources = new string[] {"Alex", "Sonya"};

            string[] analytics =
                analyser.CalculateMaxIncomeAndLowestSpendingSources(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(sources, analytics);
        }

        [Test]
        public void EmptyData_ReturnsEmptyStrings()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            string[] sources = new string[] {"", ""};

            string[] analytics =
                analyser.CalculateMaxIncomeAndLowestSpendingSources(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(sources, analytics);
        }

        [Test]
        public void SingleNoteData_ReturnsOneMaximumOtherEmpty()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            set.Add(new Transaction(new DateTime(2008, 08, 28), "Alex", 100));
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            string[] sources = new string[] {"Alex", ""};

            string[] analytics =
                analyser.CalculateMaxIncomeAndLowestSpendingSources(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(sources, analytics);
        }
    }
}