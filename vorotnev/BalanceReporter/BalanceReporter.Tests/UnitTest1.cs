using System.Collections.Generic;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class Tests
    {
        [Test]
        public void ParseDate_ValidDate()
        {
            //arrange
            List<string[]> data = new List<string[]>();
            string[] dataLine = {"11/27/2019", "Sberbank", "42268.91"};
            data.Add(dataLine);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"27.11.2019 0:00:00", "Sberbank", "42268.91"};
            expected.Add(resultLine);
            //act
            var result = Program.ParseDate(data);
            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MoneyMovesByMonths_PositiveSum()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine = {"27.11.2019 0:00:00", "Sberbank", "42268.91"};
            parsedData.Add(dataLine);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"11", "2019", "42268,91"};
            expected.Add(resultLine);
            //act
            var result = Program.MoneyMovesByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MoneyMovesByMonths_NegativeSum()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine = {"27.11.2019 0:00:00", "Sberbank", "-42268.91"};
            parsedData.Add(dataLine);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"11", "2019", "-42268,91"};
            expected.Add(resultLine);
            //act
            var result = Program.MoneyMovesByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MoneyMovesByYears_PositiveSum()
        {
            List<string[]> input = new List<string[]>();
            string[] dataLine = {"11", "2019", "42268,91"};
            input.Add(dataLine);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"2019", "42268,91"};
            expected.Add(resultLine);
            //act
            var result = Program.MoneyMovesByYears(input);
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void MoneyMovesByYears_NegativeSum()
        {
            List<string[]> input = new List<string[]>();
            string[] dataLine = {"11", "2019", "-42268,91"};
            input.Add(dataLine);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"2019", "-42268,91"};
            expected.Add(resultLine);
            //act
            var result = Program.MoneyMovesByYears(input);
            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AverageMoneyByMonths_Positive()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"27.11.2019 0:00:00", "Sberbank", "100"};
            parsedData.Add(dataLine1);
            string[] dataLine2 = {"27.11.2019 0:00:00", "Sberbank", "1000"};
            parsedData.Add(dataLine2);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"11", "2019", "550", "0"};
            expected.Add(resultLine);
            //act
            var result = Program.AverageMoneyByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void AverageMoneyByMonths_Negative()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"27.11.2019 0:00:00", "Sberbank", "-100"};
            parsedData.Add(dataLine1);
            string[] dataLine2 = {"27.11.2019 0:00:00", "Sberbank", "-1000"};
            parsedData.Add(dataLine2);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"11", "2019", "0", "-550"};
            expected.Add(resultLine);
            //act
            var result = Program.AverageMoneyByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void AverageMoneyByMonths_Mixed()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"27.11.2019 0:00:00", "Sberbank", "1000"};
            parsedData.Add(dataLine1);
            string[] dataLine2 = {"28.11.2019 0:00:00", "Sberbank", "-1000"};
            parsedData.Add(dataLine2);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"11", "2019", "1000", "-1000"};
            expected.Add(resultLine);
            //act
            var result = Program.AverageMoneyByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AverageMoneyByYears_Positive()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"11", "2019", "550", "0"};
            parsedData.Add(dataLine1);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"2019", "550", "0"};
            expected.Add(resultLine);
            //act
            var result = Program.AverageMoneyByYears(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void AverageMoneyByYears_Negative()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"11", "2019", "-550"};
            parsedData.Add(dataLine1);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"2019", "0", "-550"};
            expected.Add(resultLine);
            //act
            var result = Program.AverageMoneyByYears(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void AverageMoneyByYears_Mixed()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"11", "2019", "-550"};
            parsedData.Add(dataLine1);
            string[] dataLine2 = {"12", "2019", "560"};
            parsedData.Add(dataLine2);
            List<string[]> expected = new List<string[]>();
            string[] resultLine = {"2019", "560", "-550"};
            expected.Add(resultLine);
            //act
            var result = Program.AverageMoneyByYears(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void MaxMoneyByMonths_Incoming()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"27.11.2019 0:00:00", "Sberbank", "100"};
            parsedData.Add(dataLine1);
            string[] dataLine2 = {"27.11.2019 0:00:00", "Tinkoff", "1000"};
            parsedData.Add(dataLine2);
            string[] expected = {"1000", "0", "Tinkoff", ""};
            //act
            var result = Program.MaxMoneyByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void MaxMoneyByMonths_Outcoming()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"27.11.2019 0:00:00", "Sberbank", "-100"};
            parsedData.Add(dataLine1);
            string[] dataLine2 = {"27.11.2019 0:00:00", "Tinkoff", "-1000"};
            parsedData.Add(dataLine2);
            string[] expected = {"0", "-1000", "", "Tinkoff"};
            //act
            var result = Program.MaxMoneyByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void MaxMoneyByMonths_Mixed()
        {
            //arrange
            List<string[]> parsedData = new List<string[]>();
            string[] dataLine1 = {"27.11.2019 0:00:00", "Yandex", "-100"};
            parsedData.Add(dataLine1);
            string[] dataLine2 = {"27.11.2019 0:00:00", "Apple", "-1000"};
            parsedData.Add(dataLine2);
            string[] dataLine3 = {"27.11.2019 0:00:00", "Microsoft", "500"};
            parsedData.Add(dataLine3);
            string[] dataLine4 = {"27.11.2019 0:00:00", "Amazon", "116"};
            parsedData.Add(dataLine4);
            string[] expected = {"500", "-1000", "Microsoft", "Apple"};
            //act
            var result = Program.MaxMoneyByMonths(parsedData);
            //assert
            Assert.AreEqual(expected, result);
        }
    }
}