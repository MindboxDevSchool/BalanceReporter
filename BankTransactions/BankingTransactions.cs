using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BankTransactions
{
    public class Program
    {

        public static IEnumerable<String[]> read_transactions(String filepath)
        {
            var lines = File
                .ReadAllLines(filepath)
                .Select(x => x.Split(';'));
            return lines;
        }
        
        public static DateTime[] get_transactions_dates(IEnumerable<String[]> lines)
        {
            int lineLength = lines.First().Count();

            var csv = lines.Skip(1)
                .SelectMany(x => x)
                .Select((v, i) =>
                    new {Value = v, Index = i % lineLength})
                .ToArray();
        
            var dates = csv
                .Where(x => x.Index == 0)
                .Select(x => DateTime.ParseExact(x.Value, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture))
                .ToArray();

            return dates;

        }

        public static string[] get_transactions_purposes(IEnumerable<String[]> lines)
        {
            int lineLength = lines.First().Count();
            var csv = lines.Skip(1)
                .SelectMany(x => x)
                .Select((v, i) => new {Value = v, Index = i % lineLength})
                .ToArray();
            
            var names = csv
                .Where(x => x.Index == 1)
                .Select(x => x.Value)
                .ToArray();

            return names;
        }
        
        public static double[] get_transactions_amounts(IEnumerable<String[]> lines)
        {
            int lineLength = lines.First().Count();
            var CSV = lines.Skip(1)
                .SelectMany(x => x)
                .Select((v, i) => new {Value = v, Index = i % lineLength})
                .ToArray();
            
            var amounts = CSV
                .Where(x => x.Index == 2)
                .Select(x => Convert.ToDouble(x.Value, CultureInfo.InvariantCulture))
                .ToArray();

            return amounts;
        }

        public static List<Transaction> create_transactions(double[] pays, DateTime[] dates)
        {
            var transactions = new List<Transaction> { };
            for (int i = 0; i < pays.Length; i++)
            {
                transactions.Add(new Transaction {Date = dates[i], Amount = pays[i]});
            }

            return transactions;
        }

        public static List<String[]> group_transactions_by_date(List<Transaction> transactions)
        {
            var dateGroupedTransactions = transactions
                .Select(k => new {k.Date.Year, k.Date.Month, k.Amount})
                .GroupBy(x => new {x.Year, x.Month},
                    (key, group) => new
                    {
                        Year = key.Year,
                        Month = key.Month,
                        totalTransaction = Math.Round(group
                            .Sum(k => k.Amount), 2)
                    })
                .OrderBy(y => y.Year)
                .ThenBy(m => m.Month)
                .ToList();
            List<String[]> groupedByDateTransactions = new List<string[]>();
            foreach (var dateGroupedTransaction in dateGroupedTransactions)
            {
                String[] data = new string[]
                {
                    dateGroupedTransaction.Year.ToString(), 
                    dateGroupedTransaction.Month.ToString(),
                    dateGroupedTransaction.totalTransaction.ToString(CultureInfo.InvariantCulture)
                };
                groupedByDateTransactions.Add(data);
            }

            return groupedByDateTransactions;
        }

        public static void print_grouped_by_date_transactions(List<String[]> groupedData)
        {
            foreach (var dataCollection in groupedData)
            {
                Console.WriteLine("Year: {0} - Month: {1} - Total Sum Of Transactions: {2}",
                    dataCollection[0], 
                    dataCollection[1], 
                    dataCollection[2]);
            }
        }

        public static double maximal_income(double[] amounts)
        {
            if (amounts.Length == 0)
            {
                Console.WriteLine("No transactions founded!");
                return 0;
            }
            else
            {
                double maximalIncome = (from i in amounts where i > 0 select i).Max();
                return maximalIncome;
            }
        }
        
        public static double maximal_expense(double[] amounts)
        {
            if (amounts.Length == 0)
            {
                Console.WriteLine("No transactions founded!");
                return 0;
            }
            else
            {
                double maximalExpense = (from i in amounts where i <= 0 select i).Min();
                return maximalExpense;
            }
        }
        
        public static double average_income(double[] amounts)
        {
            if (amounts.Length == 0)
            {
                Console.WriteLine("No transactions founded!");
                return 0;
            }
            else
            {
                double averageIncome = Math.Round((from i in amounts where i > 0 select i).Average(), 2);;
                return averageIncome;
            }
        }
        
        public static double average_expense(double[] amounts)
        {
            if (amounts.Length == 0)
            {
                Console.WriteLine("No transactions founded!");
                return 0;
            }
            else
            {
                double averageExpense = Math.Round((from i in amounts where i <= 0 select i).Average(), 2);
                return averageExpense;
            }
        }
        
        public static string top_sender(double maximalIncome, string[] names, double[] amounts)
        {
            if ((amounts.Length == 0) || (names.Length == 0))
            {
                Console.WriteLine("No transactions founded!");
                return "Error 1";
            }
            else if (amounts.Length != names.Length)
            {
                Console.WriteLine("Not equal amount of sender names and transaction sums!");
                return "Error 2";
            }
            else
            {
                int maximalIncomeIndex = amounts.ToList().IndexOf(maximalIncome);
                String topSender = names[maximalIncomeIndex];
                return topSender;
            }
        }
        
        public static string top_recipient(double maximalExpense, string[] names, double[] amounts)
        {
            if ((amounts.Length == 0) || (names.Length == 0))
            {
                Console.WriteLine("No transactions founded!");
                return "Error 1";
            }
            else if (amounts.Length != names.Length)
            {
                Console.WriteLine("Not equal amount of sender names and transaction sums!");
                return "Error 2";
            }
            else
            {
                int maximalExpenseIndex = amounts.ToList().IndexOf(maximalExpense);
                String topRecipient = names[maximalExpenseIndex];
                return topRecipient;
            }
        }

        public static void print_maximums(double maximalIncome, double maximalExpense)
        {
            Console.WriteLine("Maximal income is " + maximalIncome + " $");
            Console.WriteLine("Maximal expense is " + maximalExpense + " $");
        }

        public static void print_averages(double averageIncome, double averageExpense)
        {
            Console.WriteLine("Average income is " + averageIncome + " $");
            Console.WriteLine("Average expense is " + averageExpense + " $");
        }
        
        public static void print_cost_items(string topSender, string topRecipient)
        {
            Console.WriteLine("Most money was sent from " + topSender);
            Console.WriteLine("Most money was received by " + topRecipient);
        }
        
        
        
        
        
        
        static void Main(string[] args)
        {
            String FILEPATH = "C:/Users/Злата/RiderProjects/BankTransactions/BankTransactions/stepenko_transactions.csv";
            IEnumerable<String[]> lines = read_transactions(FILEPATH);
            double[] amounts = get_transactions_amounts(lines);
            DateTime[] dates = get_transactions_dates(lines);
            string[] purposes = get_transactions_purposes(lines);
            List<Transaction> transactions = create_transactions(amounts, dates);
            List<String[]> groupedByDateTransactions = group_transactions_by_date(transactions);
            print_grouped_by_date_transactions(groupedByDateTransactions);
            double maximalIncome = maximal_income(amounts);
            double maximalExpanse = maximal_expense(amounts);
            double averageIncome = average_income(amounts);
            double averageExpanse = average_expense(amounts);
            string topSender = top_sender(maximalIncome, purposes, amounts);
            string topRecipient = top_recipient(maximalExpanse, purposes, amounts);
            print_maximums(maximalIncome, maximalExpanse);
            print_averages(averageIncome, averageExpanse);
            print_cost_items(topSender, topRecipient);




        }
        
        public class Transaction
        {
            public DateTime Date { get; set; }
            public double Amount { get; set;}
        }

    }
}