using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BalanceReporter.Helpers;

namespace BalanceReporter
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Программа запущена");
            Console.WriteLine("Введите название файла:");
            var filePath = Console.ReadLine();
            while (!File.Exists(filePath))
            {
                Console.WriteLine("Файл не найден:(");
                filePath = Console.ReadLine();
            }
            try
            {
                using (TextReader reader = new StreamReader(filePath))
                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csvReader.Configuration.Delimiter = ",";
                    var records = csvReader.GetRecords<BalanceReporterCsvParser>().ToList();

                    Print(CalculationMaker.GetAverageCostsByYear(records), "AverageCostsByYear:");
                    Print(CalculationMaker.GetAverageIncomeByYear(records), "AverageIncomeByYear:");
                    Print(CalculationMaker.GetAverageCostsByMonth(records), "AverageCostsByMonth:");
                    Print(CalculationMaker.GetAverageIncomeByMonth(records), "AverageIncomeByMonth:");
                    Print(CalculationMaker.GetMaxCostsByYear(records), "MaxCostsByYear:");
                    Print(CalculationMaker.GetMaxIncomeByYear(records), "MaxIncomeByYear:");
                    Print(CalculationMaker.GetMaxIncomeByMonth(records), "MaxIncomeByMonth:");
                    Print(CalculationMaker.GetMaxCostsByMonth(records), "MaxCostsByMonth:");
                    Print(CalculationMaker.GetCompanyNamyThatSendsMostMoneyByYear(records), 
                        "CompaniesThatSendMostMoneyByYear:");
                    Print(CalculationMaker.GetCompanyNamyThatSpendsMostMoneyByYear(records), 
                        "CompaniesThatSpendMostMoneyByYear:");
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("Файл используется другой программой");
            }
            catch (Exception e)
            {
                Console.WriteLine("Упс.. что-то пошло не так :(");
            }

        }

        private static void Print(IOrderedEnumerable<Transaction> transactions, string headline)
        {
            Console.WriteLine(headline);
            foreach (var transaction in transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}
