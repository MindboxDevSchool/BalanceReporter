using System;
using System.Collections.Generic;
using BalanceReporterLocal;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class CalculateTrafficForYearsTests
    {
        [Test]
        public void ValidData_ReturnsValidTraffic()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            set.Add(new Transaction(new DateTime(2008, 08, 28), "Alex", 100));
            set.Add(new Transaction(new DateTime(2009, 08, 28), "Sonya", -200));
            set.Add(new Transaction(new DateTime(2010, 08, 28), "Alex", 100));
            set.Add(new Transaction(new DateTime(2010, 08, 28), "Mary", -50));
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            Dictionary<string, double[]> traffic = new Dictionary<string, double[]>()
            {
                {"2008", new double[] {100, 0}},
                {"2009", new double[] {0, -200}}, 
                {"2010", new double[] {100, -50}}
            };

            Dictionary<string, double[]> analytics =
                analyser.CalculateTrafficForYears(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(traffic, analytics);
        }

        [Test]
        public void EmptyData_ReturnsZeroes()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            Dictionary<string, double[]> traffic = new Dictionary<string, double[]>();

            Dictionary<string, double[]> analytics =
                analyser.CalculateTrafficForYears(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(traffic, analytics);
        }

        [Test]
        public void SingleDate_ReturnsSingleTrafficNote()
        {
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            set.Add(new Transaction(new DateTime(2008, 08, 28), "Alex", 100));
            var analyser = new BalanceReporterLocal.BalanceAnalyser(set);
            Dictionary<string, double[]> traffic = new Dictionary<string, double[]>(){{"2008", new double[] {100, 0}}};

            Dictionary<string, double[]> analytics =
                analyser.CalculateTrafficForYears(DateTime.MinValue, DateTime.MaxValue);
            
            Assert.AreEqual(traffic, analytics);
        }
    }
}