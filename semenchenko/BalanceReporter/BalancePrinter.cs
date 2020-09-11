using System;
using System.Collections.Generic;

namespace BalanceReporterLocal
{
    class BalancePrinter
    {
        public void AskForPathInput()
        {
            Console.WriteLine("Input CSV-file path");
        }

        public void WarnInvalidPath()
        {
            Console.WriteLine("No such path! Try again.");
        }
        
        public void PrintCommandMenu()
        {
            Console.WriteLine("What would you like to do with your data?");
            Console.WriteLine("0 - over and out");
            Console.WriteLine("1 - calculate traffic for years");
            Console.WriteLine("2 - calculate traffic for months");
            Console.WriteLine("3 - calculate average income and spending");
            Console.WriteLine("4 - calculate maximum income and spending");
            Console.WriteLine("5 - calculate maximum income and spending sources");
        }

        public void WarnUnknownCommand()
        {
            Console.WriteLine("No such command! Try again.");
        }
        
        public void AskToInputDate(string date)
        {
            Console.WriteLine("Input " + date + " date:");
        }

        public void PrintTrafficData(Dictionary<string, double[]> result)
        {
            Console.WriteLine("Traffic:");
            foreach (var item in result)
            {
                Console.WriteLine(item.Key + " : " + 
                                  item.Value[0] + " as income, " + 
                                  item.Value[1] + " as spending");
            }
        }

        public void PrintAverageResults(double[] averageIncomeAndSpending)
        {
            Console.WriteLine("Averages are:");
            Console.WriteLine(averageIncomeAndSpending[0] + " as income,");
            Console.WriteLine(averageIncomeAndSpending[1] + " as spending.");
        }

        public void PrintSources(string[] sources)
        {
            Console.WriteLine("Sources are:");
            Console.WriteLine(sources[0] + " as largest income source, ");
            Console.WriteLine(sources[1] + " as largest spending source.");
        }

        public void PrintMaxResults(double[] maxIncomeAndLowestSpending)
        {
            Console.WriteLine("Limits are:");
            Console.WriteLine(maxIncomeAndLowestSpending[0] + " as largest income,");
            Console.WriteLine(maxIncomeAndLowestSpending[1] + " as largest spending.");
        }
    }
}