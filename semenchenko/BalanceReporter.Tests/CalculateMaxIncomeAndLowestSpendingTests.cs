using System;
using System.Collections.Generic;
using BalanceReporterLocal;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class CalculateMaxIncomeAndLowestSpendingTests
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
            double[] limits = new double[] {300, -200};

            double[] analytics =
                analyser.CalculateMaxIncomeAndLowestSpending(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(limits, analytics);
        }

        [Test]
        public void EmptyData_ReturnsZeroes()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            double[] limits = new double[] {0, 0};

            double[] analytics =
                analyser.CalculateMaxIncomeAndLowestSpending(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(limits, analytics);
        }

        [Test]
        public void SingleNoteData_ReturnsOneMaximumOtherZero()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            set.Add(new Transaction(new DateTime(2008, 08, 28), "Alex", 100));
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            double[] limits = new double[] {100, 0};

            double[] analytics =
                analyser.CalculateMaxIncomeAndLowestSpending(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(limits, analytics);
        }
    }
}