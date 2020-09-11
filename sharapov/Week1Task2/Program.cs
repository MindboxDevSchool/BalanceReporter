using System;
using System.IO;
using System.Text;

/*
 * Написать программу, которая парсит выписку из банковского счёта в формате csv вида
Дата;С кем была транзакция;Сумма транзакции
и выводит информацию о
- движении денежных средств по месяцам и годам [1]
- о среднем доходе и расходе [2]
- о максимальных доходах и расходах [3]
- о тех, кто присылает больше всего денег и на что больше всего денег потрачено. [4]
Точно так же - покрыть тестами, результаты в пул-реквест в свою папку вот сюда 
 */

//Все показатели давайте считать в разрезе месяцев, лет и всего времени

namespace Week1Task2 {
    internal static class Program {
        private static void Main(string[] args) {
            
            //config console
            ConfigConsoleOutput();
            
            //get path to csv
            var csvPath = Utility.GetCsvPath(args);
            
            //create transactions from csv
            var transactions = TransactionsCreator.CreateFromCsv(csvPath);
            
            //crete analyzes
            var bankInfo = new Analyzer(transactions);
            
            Console.WriteLine($"path={csvPath}");
            
            //crete report creator
            var reporter = new Reporter(bankInfo);
            
            // create reports
            var cashFlowReport = reporter.CreateReportCashFlow(); //[1]
            var averageIncomeAndOutcomeReport = reporter.CreateReportAverageIncomeAndOutcome(); //[2]
            var maxIncomeAndOutComeReport = reporter.CreateReportMaxIncomeAndOutcome(); //[3]
            var maxSpendingByVendor = reporter.CreateReportMaxIncomeAndOutcomeByVendor(); //[4]
            
            CreateFileAndAppend(@"Reports\cashFlowReport.txt", cashFlowReport);
            CreateFileAndAppend(@"Reports\averageIncomeAndOutcomeReport.txt", averageIncomeAndOutcomeReport);
            CreateFileAndAppend(@"Reports\maxIncomeAndOutComeReport.txt", maxIncomeAndOutComeReport);
            CreateFileAndAppend(@"Reports\maxSpendingByVendor.txt", maxSpendingByVendor);
        }

        private static void CreateFileAndAppend(string path, string content) {
            if (File.Exists(path)) return;
            using var sw = File.CreateText(path);
            sw.Write(content);
        }

        private static void ConfigConsoleOutput() {
            Console.OutputEncoding = Encoding.UTF8;
        }
    }
}