using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace BalanceReporterProject
{
    public class BalanceReport
    {
        static void Main(string[] args)
        {
            // user should choose one option from 3 below
            Console.WriteLine("What should the program output: 1. Money flows by month; 2. Average in and out's;" +
                              " 3. Max in and out.");
            int caseIndex = Convert.ToInt32(Console.ReadLine());
            var report = BalanceCSV("bankStatementsTask2_v3.csv");
            // the third option is not available now
            switch (caseIndex)
            {
                // group rows by year and month
                case 1:
                    ByYearAndMonth(report);
                    break;
                case 2:
                    // show average in and out transactions
                    AverageInAndOut(report);
                    break;
                default:
                    Console.WriteLine("Enter 1, 2 or 3.");
                    break;
            }

            Console.ReadLine();
        }
        
        // method "group rows by year and month"
        public static void ByYearAndMonth(List<Balance> report)
        {
            //creating the lookup list to form the output
            var lookup =
                report.ToLookup(p => new
                    { 
                        // split the "Date" column: we need to sort it by Month and Year later and use as the Key
                        Year = p.Date.Split("/")[2],
                        Month = p.Date.Split("/")[0]
                    }, 
                    // append the Company and transaction to output string
                    p => p.Name + " " + p.Transaction);
            foreach (var packageGroup in lookup)
            {
                // print the key value in the lookup
                Console.WriteLine(packageGroup.Key);
                // iterate through each value in the groped row and print its value
                foreach (string value in packageGroup)
                {
                    Console.WriteLine("    {0}", value);;
                }
            }
        }

        // method "show average in and out transactions"
        public static void AverageInAndOut(List<Balance> report)
        {
            double In = 0, Out = 0;
            // count positive .Sum members
            int countSumPlus = report.Count(x => x.Transaction > 0);
            // count negative .Sum members
            int countSumMinus = report.Count(x => x.Transaction < 0);
            foreach (var transfer in report)
            {
                // sum of positive .Sum members
                if (transfer.Transaction > 0)
                {
                    In += transfer.Transaction;
                }
                // sum of negative .Sum members
                if (transfer.Transaction < 0)
                {
                    Out -= transfer.Transaction;
                }
            }
            // output if there's in's and out's
            if (In != 0 && Out != 0)
            {
                Console.WriteLine("Average in: {0}\nAverage out: {1}", In/countSumPlus, Out/countSumMinus);
            }
            // output if there's no out's
            if (In != 0 && Out == 0)
            {
                Console.WriteLine("Average in: {0}\nThere's no out's", In/countSumPlus);
            }
            // output if there's no in's
            if (Out != 0 && In == 0)
            {
                Console.WriteLine("Average out: {0}\n There's no in's", Out/countSumMinus);
            }
        }
        
        private static List<Balance> BalanceCSV(string path)
        {
            return File.ReadAllLines(path)
                .Where(row => row.Length > 0)
                .Select(Balance.ParseRow).ToList();
        }
    }

    public class Balance
    {
        public string Date { get; set; }
        public string Name { get; set; }
        public double Transaction { get; set; }
        internal static Balance ParseRow(string row)
        {
            var columns = row.Split(',');
            return new Balance()
            {
                Date = columns[0],
                Name = columns[1],
                Transaction = double.Parse(columns[2])
            };
        }
    }
}