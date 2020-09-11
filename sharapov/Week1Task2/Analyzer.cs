using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Week1Task2 {
    public class Analyzer {
        private readonly IEnumerable<Transaction> _transactions; //readonly? //_<name> notation for readonly?


        public Analyzer(IEnumerable<Transaction> transactions) { //TODO DI of ReaderCsv
            _transactions = transactions;
        }

        //о движении денежных средств (сколько пришло)
        //total income inside inclusive interval [startDateTime, endDateTime]
        public double IncomeCashFlow(DateTime startDateTime, DateTime endDateTime) {
            ValidateDates(startDateTime, endDateTime);
            var incomeFlow = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Income)
                .Sum(t => t.Value);
            var roundedIncomeFlow = Math.Round(incomeFlow, 2, MidpointRounding.ToEven);
            return roundedIncomeFlow;
        }

        //о движении денежных средств (сколько потрачено)
        //total outcome inside inclusive interval [startDateTime, endDateTime]
        public double OutcomeCashFlow(DateTime startDateTime, DateTime endDateTime) {
            ValidateDates(startDateTime, endDateTime);
            var outcomeFlow = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Outcome)
                .Sum(t => t.Value);
            var roundedOutcomeFlow = Math.Round(outcomeFlow, 2, MidpointRounding.ToEven);
            return roundedOutcomeFlow;
        }

        //о среднем доходе 
        //return average income inside inclusive interval [startDateTime, endDateTime]
        //return double.NaN if inside interval [startDateTime, endDateTime] no elements
        //TODO ??? return null better??? probably double not best decision for business logic 
        public double AverageIncome(DateTime startDateTime, DateTime endDateTime) {
            ValidateDates(startDateTime, endDateTime);
            var average = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Income)
                .Select(t => t.Value)
                .DefaultIfEmpty(double.NaN)
                .Average();
            var result = Math.Round(average, 2, MidpointRounding.ToEven);
            return result;
        }

        //о среднем расходе 
        //return average outcome inside inclusive interval [startDateTime, endDateTime]
        //return double.NaN if inside interval [startDateTime, endDateTime] no elements
        //TODO ??? return null better??? probably double not best decision for business logic 
        public double AverageOutcome(DateTime startDateTime, DateTime endDateTime) {
            ValidateDates(startDateTime, endDateTime);
            var average = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Outcome)
                .Select(t => t.Value)
                .DefaultIfEmpty(double.NaN)
                .Average();
            var result = Math.Round(average, 2, MidpointRounding.ToEven);
            return result;
        }

        //о максимальных доходах
        //return list of transaction with max {firstN} income inside inclusive interval  [startDateTime, endDateTime]
        public IEnumerable<Transaction> MaxIncome(DateTime startDateTime, DateTime endDateTime, int firstN = 1) {
            ValidateDates(startDateTime, endDateTime);
            var result = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Income)
                .OrderByDescending(t => t.Value)
                .Take(firstN)
                .ToList();
            return result;
        }

        //о максимальных расходах
        //return list of transaction with max {firstN} outcome inside inclusive interval [startDateTime, endDateTime]
        public IEnumerable<Transaction> MaxOutcome(DateTime startDateTime, DateTime endDateTime, int firstN = 1) {
            ValidateDates(startDateTime, endDateTime);
            var result = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Outcome)
                .OrderByDescending(t => t.Value)
                .Take(firstN)
                .ToList();
            return result;
        }

        //информацию - о тех, кто присылает больше всего денег пресылает  
        //return {firstN} max income by vendor inside inclusive interval  [startDateTime, endDateTime]
        public IEnumerable<VendorInfoResult> MaxIncomeByVendor(DateTime startDateTime, DateTime endDateTime,
            int firstN = 1) {
            ValidateDates(startDateTime, endDateTime);
            var result = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Income)
                .GroupBy(t => t.VendorName)
                .Select(
                    g => new VendorInfoResult {
                        VendorName = g.Key,
                        TotalSum = g.Sum(s => s.Value)
                    })
                .OrderByDescending(s => s.TotalSum)
                .Take(firstN)
                .ToList();
            return result;
        }

        //информацию - о тех, на что больше всего денег потрачено
        //return {firstN} max outcome by vendor inside inclusive interval  [startDateTime, endDateTime]
        public IEnumerable<VendorInfoResult> MaxOutcomeByVendor(DateTime startDateTime, DateTime endDateTime,
            int firstN = 1) {
            ValidateDates(startDateTime, endDateTime);
            var result = _transactions
                .Where(t => t.Date >= startDateTime && t.Date <= endDateTime)
                .Where(t => t.TransactionType == Transaction.Type.Outcome)
                .GroupBy(t => t.VendorName)
                .Select(
                    g => new VendorInfoResult {
                        VendorName = g.Key,
                        TotalSum = g.Sum(s => s.Value)
                    })
                .OrderByDescending(s => s.TotalSum)
                .Take(firstN)
                .ToList();
            return result;
        }

        //return Earliest date 
        public DateTime FirstDate() {
            return _transactions.Min(t => t.Date);
        }

        //return Latest date 
        public DateTime LastDate() {
            return _transactions.Max(t => t.Date);
        }

        private static void ValidateDates(DateTime startDateTime, DateTime endDateTime) {
            var compareResult = DateTime.Compare(startDateTime, endDateTime);
            //startDateTime < endDateTime
            if (compareResult > 0) {
                throw new EvaluateException($@"First DateTime parameter must be earlier the second DateTime parameter. 
                                            Actually first parameter [startDateTime] equal {startDateTime}, 
                                            second parameter [endDateTime equal {endDateTime}");
            }
        }
    }
}