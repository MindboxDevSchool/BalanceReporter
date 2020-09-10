using System;
using BalanceReporter.Helpers;
using CsvHelper;
using System.Collections.Generic;
using System.Linq;
using BalanceReporter.Helpers;

namespace BalanceReporter
{
    public static class CalculationMaker
    {
        public static IOrderedEnumerable<Transaction> GetAverageCostsByYear 
            (IEnumerable<BalanceReporterCsvParser> records) 
            => GetAverageCostsOrIncomeByYear(records, e => e.SumTransaction < 0);

        public static IOrderedEnumerable<Transaction> GetAverageIncomeByYear 
            (IEnumerable<BalanceReporterCsvParser> records) 
            => GetAverageCostsOrIncomeByYear(records, e => e.SumTransaction > 0);    
        
        public static IOrderedEnumerable<Transaction> GetAverageCostsByMonth(
            IEnumerable<BalanceReporterCsvParser> records)
            => GetAverageCostsOrIncomeByMonth(records, g => g.SumTransaction < 0);
        
        public static IOrderedEnumerable<Transaction> GetAverageIncomeByMonth(
            IEnumerable<BalanceReporterCsvParser> records)
            => GetAverageCostsOrIncomeByMonth(records, g => g.SumTransaction > 0);
        
        public static IOrderedEnumerable<Transaction> GetMaxCostsByYear
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetMaxCostsOrIncomeByYear(records, g => g.SumTransaction < 0); 
        
        public static IOrderedEnumerable<Transaction> GetMaxIncomeByYear
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetMaxCostsOrIncomeByYear(records, g => g.SumTransaction > 0); 
        
        public static IOrderedEnumerable<Transaction> GetMaxIncomeByMonth
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetMaxCostsOrIncomeByMonth(records, g => g.SumTransaction > 0); 
        
        public static IOrderedEnumerable<Transaction> GetMaxCostsByMonth
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetMaxCostsOrIncomeByMonth(records, g => g.SumTransaction < 0);

        public static IOrderedEnumerable<Transaction> GetCompanyNamyThatSendsMostMoneyByYear
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetCompanyNamyThatSendsOrSpendMostMoneyByYear(records, g => g.SumTransaction > 0); 
        
        public static IOrderedEnumerable<Transaction> GetCompanyNamyThatSpendsMostMoneyByYear
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetCompanyNamyThatSendsOrSpendMostMoneyByYear(records, g => g.SumTransaction < 0);

        public static IOrderedEnumerable<Transaction> GetCompanyNamyThatSendsMostMoneyByMonth
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetCompanyNamyThatSendsOrOnSpendMostMoneyByMonth(records, g => g.SumTransaction > 0); 
        
        public static IOrderedEnumerable<Transaction> GetCompanyNamyThatSpendMostMoneyByMonth
            (IEnumerable<BalanceReporterCsvParser> records)
            => GetCompanyNamyThatSendsOrOnSpendMostMoneyByMonth(records, g => g.SumTransaction < 0); 
        private static IOrderedEnumerable<Transaction> GetCompanyNamyThatSendsOrOnSpendMostMoneyByMonth
            (IEnumerable<BalanceReporterCsvParser> records, Func<BalanceReporterCsvParser, bool> filter)
        {
            var t = records
                .Where(filter)
                .GroupBy(e => new { e.Date.Year, e.Date.Month, e.OrganizationTransaction })
                .Select(e => new Transaction()
                    { Value = e.Sum(p => p.SumTransaction), 
                        Year = e.Key.Year, 
                        Organization = e.Key.OrganizationTransaction, 
                        Month = e.Key.Month });
            return t
                 .Where(e => e.Value == t.Where(f => f.Year == e.Year && f.Month == e.Month).Max(q => q.Value))
                 .OrderBy(e => e.Year)
                 .ThenBy(k => k.Month);
        }
        
        private static IOrderedEnumerable<Transaction> GetCompanyNamyThatSendsOrSpendMostMoneyByYear
            (IEnumerable<BalanceReporterCsvParser> records, Func<BalanceReporterCsvParser, bool> filter)
        {
            var t = records
                .Where(filter)
                .GroupBy(e => new { e.Date.Year, e.OrganizationTransaction })
                .Select(e => 
                    new Transaction()
                    {
                        Value = e.Sum(p => p.SumTransaction), 
                        Year = e.Key.Year, 
                        Organization = e.Key.OrganizationTransaction
                    });
            return t
                .Where(e => e.Value == t.Where(f => f.Year == e.Year).Max(q => q.Value))
                .OrderBy(e => e.Year);
        }
        
        private static IOrderedEnumerable<Transaction> GetAverageCostsOrIncomeByYear
            (IEnumerable<BalanceReporterCsvParser> records, Func<BalanceReporterCsvParser,bool> filter)
        {
            return records
                .Where(filter)
                .GroupBy(e => e.Date.Year)
                .Select(g => new Transaction()
                    { Value = g.Average(p => p.SumTransaction), Year = g.Key })
                .OrderBy(e => e.Year);
        }
        
        private static IOrderedEnumerable<Transaction> GetAverageCostsOrIncomeByMonth
            (IEnumerable<BalanceReporterCsvParser> records, Func<BalanceReporterCsvParser, bool> filter)
        {
            return records
                .Where(filter)
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new Transaction()
                    { Value = g.Average(p => p.SumTransaction), Year = g.Key.Year, Month = g.Key.Month })
                .OrderBy(e => e.Year)
                .ThenBy(e => e.Month);
        }
        
        private static IOrderedEnumerable<Transaction> GetMaxCostsOrIncomeByYear
            (IEnumerable<BalanceReporterCsvParser> records, Func<BalanceReporterCsvParser, bool> filter)
        {
            return records
                .Where(filter)
                .GroupBy(e => e.Date.Year)
                .Select(g => new Transaction()
                    { Value = g.Max(p => Math.Abs(p.SumTransaction)), Year = g.Key })
                .OrderBy(e => e.Year);
        }
        
        private static IOrderedEnumerable<Transaction> GetMaxCostsOrIncomeByMonth
            (IEnumerable<BalanceReporterCsvParser> records, Func<BalanceReporterCsvParser, bool> filter)
        {
            return records
                .Where(filter)
                .GroupBy(e => new { e.Date.Year, e.Date.Month })
                .Select(g => new Transaction()
                    { Value = g.Max(p => Math.Abs(p.SumTransaction)), Year = g.Key.Year, Month = g.Key.Month })
                .OrderBy(e => e.Year)
                .ThenBy(e => e.Month);
        }
    }
}