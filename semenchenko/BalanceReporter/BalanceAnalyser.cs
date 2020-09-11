using System;
using System.Collections.Generic;

namespace BalanceReporterLocal
{
    public class BalanceAnalyser
    {
        private readonly HashSet<Transaction> _transactionCatalogue;
        
        public BalanceAnalyser(HashSet<Transaction> container)
        {
            this._transactionCatalogue = container;
        }

        public Dictionary<string, double[]> CalculateTrafficForYears(DateTime begin, DateTime end)
        {
            var traffic = new Dictionary<string, double[]>();
            foreach(Transaction transaction in _transactionCatalogue)
            {
                if (transaction.Date >= begin && transaction.Date <= end)
                {
                    if (!traffic.ContainsKey(transaction.Date.Year.ToString()))
                    {
                        traffic[transaction.Date.Year.ToString()] = new double[2];
                    }

                    if (transaction.Sum > 0)
                    {
                        traffic[transaction.Date.Year.ToString()][0] += transaction.Sum;
                    }
                    else
                    {
                        traffic[transaction.Date.Year.ToString()][1] += transaction.Sum;
                    }
                }
            }
            return traffic;
        }

        public Dictionary<string, double[]> CalculateTrafficForMonths(DateTime begin, DateTime end)
        {
            var traffic = new Dictionary<string, double[]>();
            foreach(Transaction transaction in _transactionCatalogue)
            {
                if (transaction.Date >= begin && transaction.Date <= end)
                {
                    if (!traffic.ContainsKey(transaction.Date.Month + "/" + transaction.Date.Year))
                    {
                        traffic[transaction.Date.Month + "/" + transaction.Date.Year] = new double[2];
                    }
                    if (transaction.Sum > 0)
                    {
                        traffic[transaction.Date.Month + "/" + transaction.Date.Year][0]  += transaction.Sum;
                    }
                    else
                    {
                        traffic[transaction.Date.Month + "/" + transaction.Date.Year][1]  += transaction.Sum;
                    }
                }
            }
            return traffic;
        }
        
        

        public double[] CalculateAverageIncomeAndSpending(DateTime begin, DateTime end)
        {
            double[] sums = new double[] {0, 0};
            int[] counts = new int[] {0, 0};
            foreach(Transaction transaction in _transactionCatalogue)
            {
                if (transaction.Date >= begin && transaction.Date <= end)
                {
                    if (transaction.Sum > 0)
                    {
                        sums[0] += transaction.Sum;
                        counts[0]++;
                    }
                    else if (transaction.Sum < 0)
                    {
                        sums[1] += transaction.Sum;
                        counts[1]++;
                    }
                }
            }
            double[] averages = new double[2];
            averages[0] = counts[0] == 0 ? 0 : sums[0] / counts[0];
            averages[1] = counts[1] == 0 ? 0 : sums[1] / counts[1];
            return averages;
        }
        
        public double[] CalculateMaxIncomeAndLowestSpending(DateTime begin, DateTime end)
        {
            double[] limits = new double[]{ double.MinValue, double.MaxValue };
            foreach(Transaction transaction in _transactionCatalogue)
            {
                if (transaction.Date >= begin && transaction.Date <= end)
                {
                    if (transaction.Sum >= 0 && transaction.Sum > limits[0])
                    {
                        limits[0] = transaction.Sum;
                    }
                    if (transaction.Sum <= 0 && transaction.Sum < limits[1])
                    {
                        limits[1] = transaction.Sum;
                    }
                }
            }

            if (Math.Abs(limits[0] - double.MinValue) < Double.Epsilon)
            {
                limits[0] = 0;
            }
            if (Math.Abs(limits[1] - double.MaxValue) < Double.Epsilon)
            {
                limits[1] = 0;
            }
            return limits;
        }
        
        public string[] CalculateMaxIncomeAndLowestSpendingSources(DateTime begin, DateTime end)
        {
            string[] sources = new string[] {"", ""};
            double[] limits = new double[]{ double.MinValue, double.MaxValue };
            foreach(Transaction transaction in _transactionCatalogue)
            {
                if (transaction.Date >= begin && transaction.Date <= end)
                {
                    if (transaction.Sum >= 0 && transaction.Sum > limits[0])
                    {
                        sources[0] = transaction.Source;
                        limits[0] = transaction.Sum;
                    }
                    if (transaction.Sum <= 0 && transaction.Sum < limits[1])
                    {
                        sources[1] = transaction.Source;
                        limits[1] = transaction.Sum;
                    }
                }
            }
            return sources;
        }
    }
}