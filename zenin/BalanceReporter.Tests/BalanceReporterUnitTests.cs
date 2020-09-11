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
        public void PositiveAmountAndPeriodExistsInTable_AddAmountInMovementTable_TableWithOnePositiveRecord()
        {
            //arrange
            var period = "June 2020";
            var amount = 1234;
            var initialTable = BalanceReporter.CreateMovementsTable();
            initialTable.Rows.Add("June 2020", 0.0, 0.0, 0, 0);
            //act
            var table = BalanceReporter.AddAmountInMovementTable(initialTable, period, amount, 1);
            //assert
            Assert.AreEqual("June 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("1234", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("0", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("1", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("0", table.Rows[0]["negative_transactions"].ToString());
        }
        
        [Test]
        public void NegativeAmountAndPeriodExistsInTable_AddAmountInMovementTable_TableWithOneNegativeRecord()
        {
            //arrange
            var period = "June 2020";
            var amount = -1234;
            var initialTable = BalanceReporter.CreateMovementsTable();
            initialTable.Rows.Add("June 2020", 0.0, 0.0, 0, 0);
            //act
            var table = BalanceReporter.AddAmountInMovementTable(initialTable, period, amount, 1);
            //assert
            Assert.AreEqual("June 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("0", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("-1234", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("0", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("1", table.Rows[0]["negative_transactions"].ToString());
        }
        
        
        [Test]
        public void PositiveAmountAndPeriodNotInTable_AddAmountInMovementTable_TableWithOnePositiveRecord()
        {
            //arrange
            var period = "May 2020";
            var amount = 1234;
            
            var emptyTable = BalanceReporter.CreateMovementsTable();
            //act
            var table = BalanceReporter.AddAmountInMovementTable(emptyTable, period, amount, 1);
            //assert
            Assert.AreEqual("May 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("1234", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("0", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("1", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("0", table.Rows[0]["negative_transactions"].ToString());
        }
        
        [Test]
        public void NegativeAmountAndPeriodNotInTable_AddAmountInMovementTable_TableWithOneNegativeRecord()
        {
            //arrange
            var period = "May 2020";
            var amount = -1234;
            var emptyTable = BalanceReporter.CreateMovementsTable();
            //act
            var table = BalanceReporter.AddAmountInMovementTable(emptyTable, period, amount, 1);
            //assert
            Assert.AreEqual("May 2020", table.Rows[0]["period"].ToString());
            Assert.AreEqual("0", table.Rows[0]["earned"].ToString());
            Assert.AreEqual("-1234", table.Rows[0]["spent"].ToString());
            Assert.AreEqual("0", table.Rows[0]["positive_transactions"].ToString());
            Assert.AreEqual("1", table.Rows[0]["negative_transactions"].ToString());
        }
        
        [Test]
        public void DataTableFromCSV_CreateMonthsMovementsTable_CorrectJanuary2019RowOfMonthsMovementTable()
        {
            //arrange
            string fileName = "data.csv";
            var dataTable = BalanceReporter.FromCSVtoDataTable(fileName);
            //act
            var firstRowOfMonthsMovementsTable = BalanceReporter.CreateMonthsMovementsTable(dataTable).Rows[0];
            //assert
            Assert.AreEqual("January 2019", firstRowOfMonthsMovementsTable["period"].ToString());
            Assert.AreEqual("100000", firstRowOfMonthsMovementsTable["earned"].ToString());
            Assert.AreEqual("-5000", firstRowOfMonthsMovementsTable["spent"].ToString());
            Assert.AreEqual("2", firstRowOfMonthsMovementsTable["positive_transactions"].ToString());
            Assert.AreEqual("1", firstRowOfMonthsMovementsTable["negative_transactions"].ToString());
        }
        
        [Test]
        public void DataTableFromCSV_CreateYearsMovementsTable_Correct2019RowOfYearsMovementTable()
        {
            //arrange
            string fileName = "data.csv";
            var dataTable = BalanceReporter.FromCSVtoDataTable(fileName);
            var monthsMovementsTable = BalanceReporter.CreateMonthsMovementsTable(dataTable);
            //act
            var firstRowOfYearsMovementsTable = BalanceReporter.CreateYearsMovementsTable(monthsMovementsTable).Rows[0];
            //assert
            Assert.AreEqual("2019", firstRowOfYearsMovementsTable["period"].ToString());
            Assert.AreEqual("200000", firstRowOfYearsMovementsTable["earned"].ToString());
            Assert.AreEqual("-25000", firstRowOfYearsMovementsTable["spent"].ToString());
            Assert.AreEqual("4", firstRowOfYearsMovementsTable["positive_transactions"].ToString());
            Assert.AreEqual("5", firstRowOfYearsMovementsTable["negative_transactions"].ToString());
        }
    }
}