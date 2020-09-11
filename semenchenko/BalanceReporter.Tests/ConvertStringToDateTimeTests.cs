using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class ConvertStringToDateTimeTests
    {
        [Test]
        public void ValidDateWithSlashes_ReturnsValidDateTime()
        {
            string date = "08/28/2008";
            var parser = new BalanceReporterLocal.BalanceParser();

            DateTime dateTime = parser.ConvertStringToDateTime(date);
            DateTime correctDateTime = new DateTime(2008, 08, 28);
            
            Assert.AreEqual(correctDateTime, dateTime);
        }
        
        [Test]
        public void ValidDateWithDots_ReturnsValidDateTime()
        {
            string date = "08.28.2008";
            var parser = new BalanceReporterLocal.BalanceParser();

            DateTime dateTime = parser.ConvertStringToDateTime(date);
            DateTime correctDateTime = new DateTime(2008, 08, 28);
            
            Assert.AreEqual(correctDateTime, dateTime);
        }

        [Test]
        public void InverseDateWithSlashes_ReturnsValidDateTime()
        {
            string date = "2008/08/28";
            var parser = new BalanceReporterLocal.BalanceParser();

            DateTime dateTime = parser.ConvertStringToDateTime(date);
            DateTime correctDateTime = new DateTime(2008, 08, 28);
            
            Assert.AreEqual(correctDateTime, dateTime);
        }
        
        [Test]
        public void InverseDateWithDots_ReturnsValidDateTime()
        {
            string date = "2008.08.28";
            var parser = new BalanceReporterLocal.BalanceParser();

            DateTime dateTime = parser.ConvertStringToDateTime(date);
            DateTime correctDateTime = new DateTime(2008, 08, 28);
            
            Assert.AreEqual(correctDateTime, dateTime);
        }
        
        [Test]
        public void InvalidDate_RisesException()
        {
            string date = "2008.28.28";
            var parser = new BalanceReporterLocal.BalanceParser();
            
            Assert.Throws<FormatException>((() => parser.ConvertStringToDateTime(date)));
        }
        
        [Test]
        public void NotADate_RisesException()
        {
            string date = "rfbfrtb";
            var parser = new BalanceReporterLocal.BalanceParser();
            
            Assert.Throws<FormatException>((() => parser.ConvertStringToDateTime(date)));
        }
        
        [Test]
        public void EmptyString_RisesException()
        {
            string date = "";
            var parser = new BalanceReporterLocal.BalanceParser();
            
            Assert.Throws<FormatException>((() => parser.ConvertStringToDateTime(date)));
        }
    }
}