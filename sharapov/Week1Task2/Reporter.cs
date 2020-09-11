using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Week1Task2 {
    //Reports generator
    //each function in class spaghetti code for each report type  
    //4 different reports => 4 separate methods for generate reports 
    internal class Reporter {
        private readonly Analyzer _analyzer; //TODO readonly?

        private const string
            OutputDateFormat = "MM/dd/yyyy"; //TODO overall formatting can work not properly on wide doubles

        private const int
            LeftAlignments = -30; //TODO should be max length from .CSV column name [С кем была транзакция]

        internal Reporter(Analyzer analyzerSource) {
            _analyzer = analyzerSource;
        }

        internal string CreateReportCashFlow() {
            var reportResult = new StringBuilder("CashFlow Report\n");

            var firstDate = _analyzer.FirstDate();
            var lastDate = _analyzer.LastDate();

            //income cashflow by all time             
            reportResult.Append(
                $"Average income for all time : between {firstDate.ToString(OutputDateFormat)} and between {lastDate.ToString(OutputDateFormat)}\n");
            reportResult.Append($"{_analyzer.IncomeCashFlow(firstDate, lastDate)}\n");

            //income cashflow by years
            reportResult.Append($"Average income by years\n");
            reportResult.Append($"Date     Value \n");
            foreach (var date in YearsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfYear(date);
                var incomeCashFlow = _analyzer.IncomeCashFlow(first, last);
                if (incomeCashFlow == 0) {
                    continue;
                }
                reportResult.Append($"{date:yyyy}     {incomeCashFlow}\n");
            }

            //income cashflow by month
            reportResult.Append($"Average income by month\n");
            reportResult.Append($"Date     Value \n");
            foreach (var date in MonthsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfMonth(date);
                var incomeCashFlow = _analyzer.IncomeCashFlow(first, last);
                if (incomeCashFlow == 0) {
                    continue;
                }
                reportResult.Append($"{date:MM/yyyy}  {incomeCashFlow}\n");
            }
            
            //outcome cashflow by all time             
            reportResult.Append(
                $"Average outcome for all time : between {firstDate.ToString(OutputDateFormat)} and between {lastDate.ToString(OutputDateFormat)}\n");
            reportResult.Append($"{_analyzer.OutcomeCashFlow(firstDate, lastDate)}\n");

            //outcome cashflow by years
            reportResult.Append($"Average outcome by years\n");
            reportResult.Append($"Date     Value \n");
            foreach (var date in YearsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfYear(date);
                var outcomeCashFlow = _analyzer.OutcomeCashFlow(first, last);
                if (outcomeCashFlow == 0) {
                    continue;
                }
                reportResult.Append($"{date:yyyy}     {outcomeCashFlow}\n");
            }

            //outcome cashflow by month
            reportResult.Append($"Average outcome by month\n");
            reportResult.Append($"Date     Value \n");
            foreach (var date in MonthsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfMonth(date);
                var outcomeCashFlow = _analyzer.OutcomeCashFlow(first, last);
                if (outcomeCashFlow == 0) {
                    continue;
                }
                reportResult.Append($"{date:MM/yyyy}  {outcomeCashFlow}\n");
            }
            return reportResult.ToString();
        }

        internal string CreateReportAverageIncomeAndOutcome() {
            var reportResult = new StringBuilder("Average income and outcome Report\n");

            var firstDate = _analyzer.FirstDate();
            var lastDate = _analyzer.LastDate();

            //average income by all time             
            reportResult.Append(
                $"Average income for all time : between {firstDate.ToString(OutputDateFormat)} and between {lastDate.ToString(OutputDateFormat)}\n");
            var averageIncomeAllTime = _analyzer.AverageIncome(firstDate, lastDate);
            reportResult.Append($"{averageIncomeAllTime:0.00}\n");

            //average income by years
            reportResult.Append("Average income by years\n");
            reportResult.Append("Date            Value\n");
            foreach (var date in YearsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfYear(date);
                var averageIncome = _analyzer.AverageIncome(first, last);
                if (double.IsNaN(averageIncome)) {
                    continue;
                }
                reportResult.Append($"{date:yyyy}         {averageIncome:0.00}\n");
            }

            //average income by month
            reportResult.Append("Average income by month\n");
            foreach (var date in MonthsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfMonth(date);
                var averageIncome = _analyzer.AverageIncome(first, last);
                if (double.IsNaN(averageIncome)) {
                    continue;
                }
                reportResult.Append($"{date:MM/yyyy}      {averageIncome:0.00}\n");
            }

            //average out by all time             
            reportResult.Append(
                $"Average outcome for all time : between {firstDate.ToString(OutputDateFormat)} and between {lastDate.ToString(OutputDateFormat)}\n");
            var averageOutcomeAllTime = _analyzer.AverageOutcome(firstDate, lastDate);
            reportResult.Append($"{averageOutcomeAllTime:0.00}\n");

            //average outcome by years
            reportResult.Append("Average outcome by years\n");
            reportResult.Append("Date           Value\n");
            foreach (var date in YearsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfYear(date);
                var averageOutcome = _analyzer.AverageOutcome(first, last);
                if (double.IsNaN(averageOutcome)) {
                    continue;
                }
                reportResult.Append($"{date:yyyy}         {averageOutcome:0.00}\n");
            }

            //average outcome by month
            reportResult.Append("Average outcome by months\n");
            foreach (var date in MonthsBetween(firstDate, lastDate)) {
                var (first, last) = FirstAndLastDayOfMonth(date);
                var averageOutcome = _analyzer.AverageOutcome(first, last);
                if (double.IsNaN(averageOutcome)) {
                    continue;
                }
                reportResult.Append($"{date:MM/yyyy}      {averageOutcome:0.00}\n");
            }
            return reportResult.ToString();
        }

        internal string CreateReportMaxIncomeAndOutcome() {
            var reportResult = new StringBuilder("Maximum income and outcome report\n");

            //generate income and outcome reports
            var incomeReport = ReportGeneratorMaxFlow(_analyzer.MaxIncome, "income");
            var outcomeReport = ReportGeneratorMaxFlow(_analyzer.MaxOutcome, "outcome");

            //build single report
            reportResult.Append(incomeReport);
            reportResult.Append("\n");
            reportResult.Append(outcomeReport);
            return reportResult.ToString();
        }

        internal string CreateReportMaxIncomeAndOutcomeByVendor() {
            var reportResult = new StringBuilder("Max spending by vendor Report\n. Top {topNumRecords} results");

            //generate income and outcome reports
            var incomeReport = ReportGeneratorMaxFlowByVendor(_analyzer.MaxIncomeByVendor, "Income");
            var outcomeReport = ReportGeneratorMaxFlowByVendor(_analyzer.MaxOutcomeByVendor, "Outcome");

            //build single report
            reportResult.Append(incomeReport);
            reportResult.Append("\n");
            reportResult.Append(outcomeReport);

            return reportResult.ToString();
        }

        private StringBuilder ReportGeneratorMaxFlow(
            Func<DateTime, DateTime, int, IEnumerable<Transaction>> analyzeFoo, string reportTypeInfo) {
            StringBuilder reportResult = new StringBuilder();
            int topNumRecords = 3;
            var firstDate = _analyzer.FirstDate();
            var lastDate = _analyzer.LastDate();

            //max by all time. [topNumRecords] first results          
            reportResult.Append(
                $"\nMax {reportTypeInfo} for all time : between {firstDate:MM/yyyy} and {lastDate:MM/yyyy}. Top {topNumRecords} results \n");
            var topByAllTime = analyzeFoo(firstDate, lastDate, topNumRecords);
            reportResult.Append("Date    Vendor Name                    Value\n");
            foreach (var topTransaction in topByAllTime) {
                reportResult.Append(
                    $"{topTransaction.Date:MM/yyyy} {topTransaction.VendorName,LeftAlignments} {topTransaction.Value,LeftAlignments}\n");
            }

            reportResult.Append("_______________________________________________\n");

            //max by years. [topNumRecords] first results                   
            reportResult.Append(
                $"\nMax {reportTypeInfo} by years. Top {topNumRecords} results \n");
            reportResult.Append("Date    Vendor Name                    Value\n");
            foreach (var year in YearsBetween(firstDate, lastDate)) {
                var (firstDayOfYear, lastDayOfYear) = FirstAndLastDayOfYear(year);
                var topByYear = analyzeFoo(firstDayOfYear, lastDayOfYear, topNumRecords);
                foreach (var topTransaction in topByYear) {
                    reportResult.Append(
                        $"{topTransaction.Date:MM/yyyy} {topTransaction.VendorName,LeftAlignments} {topTransaction.Value,LeftAlignments}\n");
                }

                reportResult.Append("_______________________________________________\n");
            }

            //max by month. [topNumRecords] first results                   
            reportResult.Append(
                $"\nMax {reportTypeInfo} by month. Top {topNumRecords} results \n");
            reportResult.Append("Date    Vendor Name                    Value\n");
            foreach (var month in MonthsBetween(firstDate, lastDate)) {
                var (firstDayOfMonth, lastDayOfYear) = FirstAndLastDayOfMonth(month);
                var topByYear = analyzeFoo(firstDayOfMonth, lastDayOfYear, topNumRecords);
                if (!topByYear.Any()) continue;
                foreach (var topTransaction in topByYear) {
                    reportResult.Append(
                        $"{topTransaction.Date:MM/yyyy} {topTransaction.VendorName,LeftAlignments} {topTransaction.Value,LeftAlignments}\n");
                }
                reportResult.Append("_______________________________________________\n");
            }

            return reportResult;
        }

        private StringBuilder ReportGeneratorMaxFlowByVendor(
            Func<DateTime, DateTime, int, IEnumerable<VendorInfoResult>> analyzeByVendor, string reportType) {
            const int topNumRecords = 3;

            var firstDate = _analyzer.FirstDate();
            var lastDate = _analyzer.LastDate();

            StringBuilder reportResult = new StringBuilder();

            //grouped [income] sum by vendor by years   
            reportResult.Append($"{reportType} by month by Vendor\n");
            foreach (var year in YearsBetween(firstDate, lastDate)) {
                var (firstDayOfYear, lastDayOfYear) = FirstAndLastDayOfYear(year);
                foreach (var month in MonthsBetween(firstDayOfYear, lastDayOfYear)) {
                    var (firstDayOfMonth, lastDayOfMonth) = FirstAndLastDayOfMonth(month);
                    var maxByVendorByMonth = analyzeByVendor(firstDayOfMonth, lastDayOfMonth, topNumRecords);
                    if (!maxByVendorByMonth.Any()) continue; //if no income empty skip month
                    reportResult.Append($"{reportType} for : {month:MM/yyyy}\n");
                    foreach (var vendor in maxByVendorByMonth) {
                        reportResult.Append($"{vendor.VendorName,LeftAlignments} {vendor.TotalSum:0.00}\n");
                    }
                    reportResult.Append("_______________________________________________\n");
                }

                var maxIncomeByVendorForYear = analyzeByVendor(firstDayOfYear, lastDayOfYear, topNumRecords);
                reportResult.Append("_______________________________________________\n");
                reportResult.Append($"Top income for : {year:yyyy} year. Top {topNumRecords} results\n");
                foreach (var vendor in maxIncomeByVendorForYear) {
                    reportResult.Append($"{vendor.VendorName,LeftAlignments} {vendor.TotalSum:0.00}\n");
                }
            }

            reportResult.Append("_______________________________________________\n");
            reportResult.Append(
                $"{reportType} for all time by Vendor : between {firstDate.ToString(OutputDateFormat)} and {lastDate.ToString(OutputDateFormat)}.");
            reportResult.Append($" Top {topNumRecords} results\n");
            var maxByVendorByAllTime = analyzeByVendor(firstDate, lastDate, topNumRecords);
            reportResult.Append($"{"Vendor Name",LeftAlignments} {reportType}\n");
            foreach (var vendor in maxByVendorByAllTime) {
                reportResult.Append($"{vendor.VendorName,LeftAlignments} {vendor.TotalSum:0.00}\n");
            }

            return reportResult;
        }


        private static (DateTime first, DateTime last) FirstAndLastDayOfMonth(DateTime date) {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            return (firstDayOfMonth, lastDayMonth);
        }

        private static (DateTime first, DateTime last) FirstAndLastDayOfYear(DateTime date) {
            var firstDayOfMonth = new DateTime(date.Year, 1, 1);
            var lastDayMonth = firstDayOfMonth.AddYears(1).AddDays(-1);
            return (firstDayOfMonth, lastDayMonth);
        }

        private static IEnumerable<DateTime> MonthsBetween(DateTime firstDate, DateTime lastDate) {
            while (firstDate <= lastDate) {
                yield return firstDate;
                firstDate = firstDate.AddMonths(1);
            }
        }

        private static IEnumerable<DateTime> YearsBetween(DateTime firstDate, DateTime lastDate) {
            while (firstDate <= lastDate) {
                yield return firstDate;
                firstDate = firstDate.AddYears(1);
            }
        }
    }
}