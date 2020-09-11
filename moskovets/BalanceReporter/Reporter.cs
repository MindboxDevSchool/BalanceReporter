using System;

namespace BalanceReporter
{
    public class Reporter
    {
        public static void PrintReportTransactionSumsByMonth(Balance balance, DateTime from, DateTime to)
        {
            DateTime startDate = from;
            int allDayMonth = DateTime.DaysInMonth(startDate.Year,startDate.Month);
            DateTime endDate = new DateTime(startDate.Year, startDate.Month, allDayMonth);
            bool reachedToDate = false;
            while (startDate < to && !reachedToDate)
            {
                if (endDate >= to)
                {
                    endDate = to;
                    reachedToDate = true;
                }
                double incomes = balance.CalculateTotalForThePeriod(startDate, endDate, TransactionType.Income);
                double expenses = balance.CalculateTotalForThePeriod(startDate, endDate, TransactionType.Expense);
                Console.WriteLine("{0:MM/yyyy} incomes: {1,10:0.0} expenses {2,10:0.0}", startDate, incomes, expenses);
                startDate = endDate.AddDays(1);
                endDate = endDate.AddMonths(1);
            }
        }
    }
}