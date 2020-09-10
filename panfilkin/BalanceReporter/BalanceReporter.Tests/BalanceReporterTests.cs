using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class BalanceReporterTests
    {
        [Test]
        public void LoadCsvWithHeaders_ValidCsvFile_SuccessLoad()
        {
            
            Assert.Pass();
        }
        
        [Test]
        public void LoadCsvWithHeaders_NoCsvFile_ThrowsNoFileException()
        {
            Assert.Pass();
        }
        
        [Test]
        public void LoadCsvWithHeaders_CsvFileWithBadHeaders_ThrowsInvalidHeadersException()
        {
            Assert.Pass();
        }
        
        [Test]
        public void CsvStringParser_ValidStringToParse_SuccessParsed()
        {
            Assert.Pass();
        }
        
        [Test]
        public void CsvStringParser_EmptyStringToParse_SuccessParsed()
        {
            Assert.Pass();
        }
        
        [Test]
        public void CalculateStatisticsOfMovementOfFundsByMonths_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }
        
        [Test]
        public void CalculateStatisticsOfMovementOfFundsByMonths_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateStatisticsOfMovementOfFundsByYears_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateStatisticsOfMovementOfFundsByYears_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateAverageIncome_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateAverageIncome_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }


        [Test]
        public void CalculateAverageExpense_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateAverageExpense_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }


        [Test]
        public void CalculateMaximumIncome_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateMaximumIncome_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }


        [Test]
        public void CalculateMaximumExpense_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateMaximumExpense_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }


        [Test]
        public void CalculateNameWhoMaxSend_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }

        [Test]
        public void CCalculateNameWhoMaxSend_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }


        [Test]
        public void CalculateNameWhoMaxGet_ValidBankData_SuccessCalculated()
        {
            Assert.Pass();
        }

        [Test]
        public void CalculateNameWhoMaxGet_EmptyBankData_ThrowsNoDataException()
        {
            Assert.Pass();
        }



    }
}