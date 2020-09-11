using System;
using System.Collections.Generic;
using System.IO;
using BalanceReporterLocal;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class ParseFromFileTests
    {
        [Test]
        public void ValidCSV_ParsesValidData()
        {
            var parser = new BalanceReporterLocal.BalanceParser();
            string path = "/home/deckard/RiderProjects/BalanceReporterLocal/myFileSimple.csv";
            var fileStream = File.OpenRead(path);
            
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            set.Add(new Transaction(new DateTime(2005, 12, 08), "Fiji", 460.29));
            set.Add(new Transaction(new DateTime(2003, 09, 29), "Virgin Islands, U.S.", 569.61));
            set.Add(new Transaction(new DateTime(2014, 03, 13), "Liechtenstein", -715.14));
            set.Add(new Transaction(new DateTime(1968, 08, 28), "Finland", 159.65));
            set.Add(new Transaction(new DateTime(1909, 08, 21), "Virgin Islands, U.S.", -666.55));
            
            parser.ParseFromFile(fileStream);
            
            Assert.AreEqual(set, parser.GetTransactionCatalogue());
        }
        
        [Test]
        public void EmptyCSV_ParsesNothing()
        {
            var parser = new BalanceReporterLocal.BalanceParser();
            string path = "/home/deckard/RiderProjects/BalanceReporterLocal/myFileEmpty.csv";
            var fileStream = File.OpenRead(path);
            
            var set = new HashSet<BalanceReporterLocal.Transaction>();
            
            parser.ParseFromFile(fileStream);
            
            Assert.AreEqual(set, parser.GetTransactionCatalogue());
        }

        [Test]
        public void InvalidCSV_RaisesException()
        {
            var parser = new BalanceReporterLocal.BalanceParser();
            string path = "/home/deckard/RiderProjects/BalanceReporterLocal/myFileInvalid.csv";
            var fileStream = File.OpenRead(path);
            
            Assert.Throws<FormatException>((() => parser.ParseFromFile(fileStream)));
        }
    }
}