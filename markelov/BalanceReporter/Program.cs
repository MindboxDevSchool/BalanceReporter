using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;

namespace BalanceReporter
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(@"Enter the filepath of your csv file (example filePath for testing is: '..\..\..\bankStatementsTask2_v3.csv'):");
            string filePath = Console.ReadLine();
            List<Transaction> transactions = ParseCsvTransactions(filePath);
            PrintBalanceDataByDate(transactions);
        }
        
        public static bool CheckIfFileIsValid(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Specific file doesn't exist.");
                return false;
            }
            if (new FileInfo(filePath).Length == 0)
            {
                Console.WriteLine("The file is empty.");
                return false;
            }
            return true;
        }
        
        // Parse CSV file with balance data
        public static List<Transaction> ParseCsvTransactions(string filePath)
        {
            if (CheckIfFileIsValid(filePath) != true)
            {
                return null;
            }
            List<Transaction> transactions = new List<Transaction>();
            StreamReader reader = new StreamReader(filePath);
            string line = reader.ReadLine();
            while (line != null)
            {
                string[] transactionFields = line.Split(',');

                try
                {
                    string[] allowedFormats = { "MM/dd/yyyy", "MM/d/yyyy", "M/dd/yyyy", "M/d/yyyy" };
                    DateTime transactionDate = DateTime.ParseExact(transactionFields[0], 
                        allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    string transactionSource = transactionFields[1];
                    decimal transactionSum = Decimal.Parse(transactionFields[2], 
                        NumberStyles.Currency | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture);
                    
                    Transaction transaction = new Transaction(transactionDate, 
                        transactionSource, transactionSum);
                
                    transactions.Add(transaction);
                }
                catch
                {
                    Console.WriteLine("The file formatting is incorrect!");
                    return null;
                }
                line = reader.ReadLine();
            }
            reader.Close();
            return transactions;
        }
        
        // Group data by months and years
        public static void PrintBalanceDataByDate(List<Transaction> transactions)
        {
            if (transactions != null)
            {
                var transactionsByYears = GroupBalanceDataByYear(transactions);
                
                Console.WriteLine("ENTIRE HISTORY OF TRANSACTIONS:");
                PrintTransactionInfoForSelectedPeriod(transactions, true,
                    $"Info on positive transactions for all time of usage:");
                PrintTransactionInfoForSelectedPeriod(transactions, false,
                    $"Info on negative transactions for all time of usage:");
            
                foreach (var yearlyGroup in transactionsByYears)
                {
                    Console.WriteLine($"{yearlyGroup.FirstOrDefault().TransactionDate.Year}");
                    var transactionsByMonths = GroupBalanceDataByMonth(yearlyGroup);

                    PrintTransactionInfoForSelectedPeriod(yearlyGroup.ToList(), true,
                        $"Additional info on positive transactions for the period of {yearlyGroup.FirstOrDefault().TransactionDate.Year}:");
                    PrintTransactionInfoForSelectedPeriod(yearlyGroup.ToList(), false,
                        $"Additional info on negative transactions for the period of {yearlyGroup.FirstOrDefault().TransactionDate.Year}:");

                    foreach (var monthlyGroup in transactionsByMonths)
                    {
                        Console.WriteLine($"{monthlyGroup.FirstOrDefault().TransactionDate.ToString("MMMM", CultureInfo.InvariantCulture)}");
                        
                        foreach (var transaction in monthlyGroup)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        Console.WriteLine("-----------------------------------------------");
                        PrintTransactionInfoForSelectedPeriod(monthlyGroup.ToList(), true,
                            $"Additional info on positive transactions for the period of {monthlyGroup.FirstOrDefault().TransactionDate.ToString("MMMM", CultureInfo.InvariantCulture)}:");
                        PrintTransactionInfoForSelectedPeriod(monthlyGroup.ToList(), false,
                            $"Additional info on negative transactions for the period of {monthlyGroup.FirstOrDefault().TransactionDate.ToString("MMMM", CultureInfo.InvariantCulture)}:");
                    }
                }
            }
            else
            {
                Console.WriteLine("File doesn't have any transactions");
            }
        }
        
        public static void PrintTransactionInfoForSelectedPeriod(List<Transaction> selectedPeriod, bool isPositiveTransaction, string message)
        {
            List<Transaction> transactionsForSelectedPeriod = GetTransactions(selectedPeriod, isPositiveTransaction);
            Console.WriteLine(message);
            PrintAdditionalInfo(transactionsForSelectedPeriod);
        }
        
        public static void PrintAdditionalInfo(List<Transaction> transactionsForSelectedPeriod)
        {
            decimal sum = CalculateTransactionSum(transactionsForSelectedPeriod);
            Console.WriteLine($"Sum of transactions for the period: {sum}");
            decimal average = CalculateAverageTransaction(transactionsForSelectedPeriod);
            Console.WriteLine($"Average transaction for the period: {average:F2}");

            if (transactionsForSelectedPeriod.Count > 0)
            {
                List<Transaction> max = GetMaxTransaction(transactionsForSelectedPeriod);
                foreach (var t in max)
                {
                    Console.WriteLine($"Maximal transaction for the period is {t.ToString()}");
                }
                List<SourceGroup> maxSumBySource = GetSourceWithMaxTransactionSum(transactionsForSelectedPeriod);
                foreach (var sg in maxSumBySource)
                {
                    Console.WriteLine($"The source with maximal transaction sum is {sg.ToString()}");
                }
            }
            Console.WriteLine("-----------------------------------------------");
        }
        
        public static IEnumerable<IGrouping<int, Transaction>> GroupBalanceDataByYear(List<Transaction> transactions)
        {
            var transactionsByYears = from transaction in transactions
                orderby transaction.TransactionDate.Year
                group transaction by transaction.TransactionDate.Year
                into yearlyGroup
                select yearlyGroup;
            return transactionsByYears;
        }
        
        public static IEnumerable<IGrouping<int, Transaction>> GroupBalanceDataByMonth(IGrouping<int, Transaction> yearlyGroup)
        {
            var transactionsByMonths = from transaction in yearlyGroup
                orderby transaction.TransactionDate.Month
                group transaction by transaction.TransactionDate.Month
                into monthlyGroup
                select monthlyGroup;
            return transactionsByMonths;
        }
        
        public static List<Transaction> GetTransactions(List<Transaction> transactionsForSelectedPeriod, bool isPositiveTransaction)
        {
            List<Transaction> transactions = new List<Transaction>();
            if (isPositiveTransaction == true)
            {
                foreach (Transaction transaction in transactionsForSelectedPeriod)
                {
                    if (transaction.TransactionSum >= 0)
                    {
                        transactions.Add(transaction);
                    }
                }
                return transactions;
            }
            else
            {
                foreach (Transaction transaction in transactionsForSelectedPeriod)
                {
                    if (transaction.TransactionSum < 0)
                    {
                        transactions.Add(transaction);
                    }
                }
                return transactions;
            }
        }
        
        // Show summary of earnings or expenses on a given period of time
        public static decimal CalculateTransactionSum(List<Transaction> transactionsForSelectedPeriod)
        {
            decimal sum = 0;
            foreach (Transaction transaction in transactionsForSelectedPeriod)
            {
                sum += transaction.TransactionSum;
            }
            return sum;
        }
        
        // Show info about average earning or expense on a given period of time
        public static decimal CalculateAverageTransaction(List<Transaction> transactionsForSelectedPeriod)
        {
            List<decimal> transactionSums = new List<decimal>();
            foreach (Transaction transaction in transactionsForSelectedPeriod)
            {
                transactionSums.Add(transaction.TransactionSum);
            }
            
            if (transactionSums.Count < 1)
            {
                return 0;
            }
            
            decimal average = transactionSums.Average();
            return average;
        }
        
        // Show the maximum earning or expense on a given period of time
        public static List<Transaction> GetMaxTransaction(List<Transaction> transactionsForSelectedPeriod)
        {
            List<decimal> transactionSums = new List<decimal>();
            foreach (Transaction transaction in transactionsForSelectedPeriod)
            {
                transactionSums.Add(transaction.TransactionSum);
            }
            
            decimal max = transactionSums.Max();
            if (max < 0)
            {
                max = transactionSums.Min();
            }
            
            var maxTransaction = transactionsForSelectedPeriod.Where(t => t.TransactionSum == max);
            return maxTransaction.ToList();
        }
        
        // Show what for was spent and received the biggest amount of money
        public static List<SourceGroup> GetSourceWithMaxTransactionSum(List<Transaction> transactionsForSelectedPeriod)
        {
            var sourceGroups = transactionsForSelectedPeriod
                .GroupBy(t => t.TransactionSource)
                .Select(g => new
                {
                    name = g.Key,
                    Sum = g.Sum(t => t.TransactionSum)
                });
            
            var sourceGroupSums = from sourceGroup in sourceGroups
                select sourceGroup.Sum;
            
            decimal maxSum = sourceGroupSums.Max();
            if (maxSum < 0)
            {
                maxSum = sourceGroupSums.Min();
            }
            
            var maxSourceGroups = sourceGroups.Where(sourceGroup => sourceGroup.Sum == maxSum);
            List<SourceGroup> maxSourceGroupsList = new List<SourceGroup>();
            foreach (var maxSourceGroup in maxSourceGroups)
            {
                SourceGroup sg = new SourceGroup(maxSourceGroup.name, maxSourceGroup.Sum);
                maxSourceGroupsList.Add(sg);
            }
            return maxSourceGroupsList;
        }
    }
}