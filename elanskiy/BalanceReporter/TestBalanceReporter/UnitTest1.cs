using System.Collections.Generic;
using NUnit.Framework;
using System.IO;
using CsvHelper;
using BalanceReporter;
using BalanceReporter.Helpers;
using System.Globalization;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace TestBalanceReporter
{
    public class Tests
    {
        private List<BalanceReporterCsvParser> records;
        public List<BalanceReporterCsvParser> Records {
            get
            {
                if (records == null) 
                    Records = GetRecords();
                return records;
            }
            set { records = value; }
        }

        private List<BalanceReporterCsvParser> GetRecords()
        {
            using(TextReader reader = new StreamReader("../../../TestFiles/accountStatementTest.csv"))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.Delimiter = ",";
                return csvReader.GetRecords<BalanceReporterCsvParser>().ToList();
            }
        }

        [Test]
        public void DefaultRecords_GetAverageCostsByYear_4CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetAverageCostsByYear(Records).ToList();
            
            //assert
            Assert.AreEqual(-2337.8016666666667d, result[0].Value); 
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(-2193.285, result[1].Value);
            Assert.AreEqual(2020, result[1].Year);
        }
        
        [Test]
        public void DefaultRecords_GetAverageIncomeByYear_4CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetAverageIncomeByYear(Records).ToList();
            
            //assert
            Assert.AreEqual(39673.78666666667, result[0].Value); 
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(4575.69, result[1].Value);
            Assert.AreEqual(2020, result[1].Year);
        }
        
        [Test]
        public void DefaultRecords_GetAverageCostsByMonth_2CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetAverageCostsByMonth(Records).ToList();
            
            //assert
            Assert.AreEqual(-2885, result[1].Value); 
            Assert.AreEqual(2019, result[1].Year);
            Assert.AreEqual(12, result[1].Month);
        }
        
        [Test]
        public void DefaultRecords_GetAverageIncomeByMonth_3CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetAverageIncomeByMonth(Records).ToList();
            
            //assert
            Assert.AreEqual(4575.69, result[1].Value); 
            Assert.AreEqual(2020, result[1].Year);
            Assert.AreEqual(1, result[1].Month);
        }
        
        [Test]
        public void DefaultRecords_GetMaxCostsByYear_4CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetMaxCostsByYear(Records).ToList();
            
            //assert
            Assert.AreEqual(4981.01, result[0].Value); 
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(3482.93, result[1].Value); 
            Assert.AreEqual(2020, result[1].Year);
        }
        
        [Test]
        public void DefaultRecords_GetMaxIncomeByYear_4CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetMaxIncomeByYear(Records).ToList();
            
            //assert
            Assert.AreEqual(53191.45, result[0].Value); 
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(4575.69, result[1].Value); 
            Assert.AreEqual(2020, result[1].Year);
        }
        
        [Test]
        public void DefaultRecords_GetMaxIncomeByMonth_6CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetMaxIncomeByMonth(Records).ToList();
            
            //assert
            Assert.AreEqual(53191.45, result[0].Value); 
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(11, result[0].Month);
            Assert.AreEqual(4575.69, result[1].Value); 
            Assert.AreEqual(2020, result[1].Year);
            Assert.AreEqual(1, result[1].Month);
        }
        
        [Test]
        public void DefaultRecords_GetMaxCostsByMonth_6CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetMaxCostsByMonth(Records).ToList();
            
            //assert
            Assert.AreEqual(4981.01, result[0].Value); 
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(11, result[0].Month);
            Assert.AreEqual(4617.05, result[1].Value); 
            Assert.AreEqual(2019, result[1].Year);
            Assert.AreEqual(12, result[1].Month);
        }
        
        [Test]
        public void DefaultRecords_GetCompanyNamesThatSendMostMoneyByYear_6CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetCompanyNamyThatSendsMostMoneyByYear(Records).ToList();
            
            //assert
            Assert.AreEqual(76752.45, result[0].Value); 
            Assert.AreEqual("Tinkoff", result[0].Organization);
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(4575.69, result[1].Value); 
            Assert.AreEqual("Sberbank", result[1].Organization);
            Assert.AreEqual(2020, result[1].Year);
        }
        
        [Test]
        public void DefaultRecords_GetCompanyNamesThatSpendMostMoneyByYear_2CorrectResults()
        {
            //arrange

            //act
            var result = CalculationMaker.GetCompanyNamyThatSpendsMostMoneyByYear(Records).ToList();
            
            //assert
            Assert.AreEqual(-132.98, result[0].Value); 
            Assert.AreEqual("DodoPizza", result[0].Organization);
            Assert.AreEqual(2019, result[0].Year);
            Assert.AreEqual(-1236.03, result[1].Value); 
            Assert.AreEqual("MODIS", result[1].Organization);
            Assert.AreEqual(2020, result[1].Year);
        }
        
    }
}