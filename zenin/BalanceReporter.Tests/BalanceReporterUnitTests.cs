using System.Data;
using  System.Linq;
using NUnit.Framework;

namespace BalanceReporter.Tests
{
    public class BalanceReporterTests
    {
        [Test]
        public void NameOfCSVFile_FromCSVtoDataTable_CorrectFirstRowOfTable()
        {
            //arrange
            string fileName = "data.csv";
            //act
            var firstRow = BalanceReporter.FromCSVtoDataTable(fileName).Rows[0];
            //assert
            Assert.AreEqual("04.01.2019", firstRow["date"].ToString());
            Assert.AreEqual("Perekrestok", firstRow["name"].ToString());
            Assert.AreEqual("-5000", firstRow["amount"].ToString());
        }
        
        [Test]
        public void DataTableFromCSVFileAndValueFromIt_FindIndexOfStringInColumn_CorrectIndex()
        {
            //arrange
            string fileName = "data.csv";
            var table = BalanceReporter.FromCSVtoDataTable(fileName);
            var value = "Ozon";
            var column = "name";
            //act
            var index = BalanceReporter.FindIndexOfStringInColumn(table, value, column);
            //assert
            Assert.AreEqual(8, index);
        }
        
        [Test]
        public void DataTableFromCSVFileAndValueNotInTable_FindIndexOfStringInColumn_IndexIsMinusOne()
        {
            //arrange
            string fileName = "data.csv";
            var table = BalanceReporter.FromCSVtoDataTable(fileName);
            var value = "NoSuchValueInTable";
            var column = "name";
            //act
            var index = BalanceReporter.FindIndexOfStringInColumn(table, value, column);
            //assert
            Assert.AreEqual(-1, index);
        }
        
        [Test]
        public void PositiveAmountAndPeriodExistsInTable_AddAmountInBalanceTable_TableWithOnePositiveRecord()
        {
            //arrange
            var period = "June 2020";
            var amount = 1234;
            var initialTable = BalanceReporter.CreateBalanceTable();
            initialTable.Rows.Add("June 2020", 0.0, 0.0, 0, 0, 0.0, 0.0, "None", "None");
            //act
            var table = BalanceReporter.AddAmountInBalanceTable(initialTable, period, amount, 1);
            //assert
            Assert.AreEqual("June 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("1234", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("0", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("1", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("0", table.Rows[0]["negative_transactions"].ToString());
        }
        
        [Test]
        public void NegativeAmountAndPeriodExistsInTable_AddAmountInBalanceTable_TableWithOneNegativeRecord()
        {
            //arrange
            var period = "June 2020";
            var amount = -1234;
            var initialTable = BalanceReporter.CreateBalanceTable();
            initialTable.Rows.Add("June 2020", 0.0, 0.0, 0, 0, 0.0, 0.0, "None", "None");
            //act
            var table = BalanceReporter.AddAmountInBalanceTable(initialTable, period, amount, 1);
            //assert
            Assert.AreEqual("June 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("0", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("-1234", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("0", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("1", table.Rows[0]["negative_transactions"].ToString());
        }
        
        
        [Test]
        public void PositiveAmountAndPeriodNotInTable_AddAmountInBalanceTable_TableWithOnePositiveRecord()
        {
            //arrange
            var period = "May 2020";
            var amount = 1234;
            
            var emptyTable = BalanceReporter.CreateBalanceTable();
            //act
            var table = BalanceReporter.AddAmountInBalanceTable(emptyTable, period, amount, 1);
            //assert
            Assert.AreEqual("May 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("1234", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("0", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("1", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("0", table.Rows[0]["negative_transactions"].ToString());
        }
        
        [Test]
        public void NegativeAmountAndPeriodNotInTable_AddAmountInBalanceTable_TableWithOneNegativeRecord()
        {
            //arrange
            var period = "May 2020";
            var amount = -1234;
            var emptyTable = BalanceReporter.CreateBalanceTable();
            //act
            var table = BalanceReporter.AddAmountInBalanceTable(emptyTable, period, amount, 1);
            //assert
            Assert.AreEqual("May 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("0", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("-1234", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("0", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("1", table.Rows[0]["negative_transactions"].ToString());
        }
        
        [Test]
        public void DataTableFromCSV_CreateMonthsBalanceTable_CorrectJanuary2019RowOfMonthsMovementTable()
        {
            //arrange
            string fileName = "data.csv";
            var dataTable = BalanceReporter.FromCSVtoDataTable(fileName);
            //act
            var firstRowOfMonthsBalanceTable = BalanceReporter.CreateMonthsBalanceTable(dataTable).Rows[0];
            //assert
            Assert.AreEqual("January 2019", firstRowOfMonthsBalanceTable["period"].ToString());
            Assert.AreEqual("100000", firstRowOfMonthsBalanceTable["earned"].ToString());
            Assert.AreEqual("-5000", firstRowOfMonthsBalanceTable["spent"].ToString());
            Assert.AreEqual("2", firstRowOfMonthsBalanceTable["positive_transactions"].ToString());
            Assert.AreEqual("1", firstRowOfMonthsBalanceTable["negative_transactions"].ToString());
            Assert.AreEqual("50000", firstRowOfMonthsBalanceTable["max_earnings"].ToString());
            Assert.AreEqual("-5000", firstRowOfMonthsBalanceTable["max_costs"].ToString());
        }
        
        [Test]
        public void DataTableFromCSV_CreateYearsBalanceTable_Correct2019RowOfYearsMovementTable()
        {
            //arrange
            string fileName = "data.csv";
            var dataTable = BalanceReporter.FromCSVtoDataTable(fileName);
            var monthsBalanceTable = BalanceReporter.CreateMonthsBalanceTable(dataTable);
            //act
            var firstRowOfYearsBalanceTable = BalanceReporter.CreateYearsBalanceTable(monthsBalanceTable).Rows[0];
            //assert
            Assert.AreEqual("2019", firstRowOfYearsBalanceTable["period"].ToString());
            Assert.AreEqual("200000", firstRowOfYearsBalanceTable["earned"].ToString());
            Assert.AreEqual("-25000", firstRowOfYearsBalanceTable["spent"].ToString());
            Assert.AreEqual("4", firstRowOfYearsBalanceTable["positive_transactions"].ToString());
            Assert.AreEqual("5", firstRowOfYearsBalanceTable["negative_transactions"].ToString());
            Assert.AreEqual("100000", firstRowOfYearsBalanceTable["max_earnings"].ToString());
            Assert.AreEqual("-16000", firstRowOfYearsBalanceTable["max_costs"].ToString());
        }
    }
}