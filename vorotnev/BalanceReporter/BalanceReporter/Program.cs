using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            StartWarning();
            var data = Parse();
            var parsedData = ParseDate(data);
            CountStats(parsedData);
        }

        static void StartWarning()
        {
            Console.WriteLine("Поместите csv файл с входными данными в папку с программой под именем input.csv.");
            Console.WriteLine("Для продолжения нажмите Enter.");
        }
        
        static List<string[]> Parse()
        {
            string path = @"input.csv";
            List<string[]> data = new List<string[]>();
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] units = line.Split(',');
                    int i = 0;
                    string[] dataLine = new string[3];
                    foreach (var unit in units)
                    {
                        dataLine[i] = unit;
                        i++;
                    }
                    data.Add(dataLine);
                }
            }
            return data;
        }

        static List<string[]> ParseDate(List<string[]> data)
        {
            List<string[]> result = new List<string[]>();
            foreach (var line in data)
            {
                var date = DateTime.ParseExact(line[0], "M/d/yyyy", null);
                string[] dataLine = { date.ToString(), line[1], line[2] }; 
                result.Add(dataLine);
            }

            return result;
        }
        
        static void CountStats(List<string[]> data)
        {
            var moneyMovesByMonths = MoneyMovesByMonths(data);
            MoneyMovesByYears(moneyMovesByMonths);
        }

        static List<string[]> MoneyMovesByMonths(List<string[]> parsedData)
        {
            var minimalDateTime = DateTime.MinValue;
            var minimalMonth = DateTime.MinValue.Month;
            var minimalYear = DateTime.MinValue.Year;
            var lastMonth = DateTime.MinValue.Month;
            var lastYear = DateTime.MinValue.Year;
            List<string[]> moneyMoves = new List<string[]>();
            for (int i = 0; i < parsedData.Count; i++)
            {
                var currentMonth = DateTime.Parse(parsedData[i][0]).Month;
                var currentYear = DateTime.Parse(parsedData[i][0]).Year;
                var currentDateTime = DateTime.Parse(parsedData[i][0]);
                if ((currentDateTime > minimalDateTime) && (currentMonth > lastMonth || currentYear > lastYear))
                {
                    minimalDateTime = DateTime.Parse(parsedData[i][0]);
                    minimalMonth = DateTime.Parse(parsedData[i][0]).Month;
                    minimalYear = DateTime.Parse(parsedData[i][0]).Year;
                    double sum = 0;
                    for (int j = i; j < parsedData.Count; j++)
                    {
                        var thisDateTime = DateTime.Parse(parsedData[j][0]);
                        var thisMonth = DateTime.Parse(parsedData[j][0]).Month;
                        var thisYear = DateTime.Parse(parsedData[j][0]).Year;
                        lastMonth = thisMonth;
                        lastYear = thisYear;
                        if (thisMonth == currentMonth && thisYear == currentYear)
                        {
                            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                            sum +=  double.Parse(parsedData[j][2]);
                            Thread.CurrentThread.CurrentCulture = cultureInfo;
                        }
                    }
                    string[] values = {Convert.ToString(currentMonth), Convert.ToString(currentYear), Convert.ToString(sum)};
                    moneyMoves.Add(values);
                }
                lastMonth = currentMonth;
                lastYear = currentYear;
            }

            return moneyMoves;
        }

        static List<string[]> MoneyMovesByYears(List<string[]> moneyMovesByMonths)
        {
            List<string[]> moneyMoves = new List<string[]>();
            var lastYear = DateTime.MinValue.Year;
            for (int i = 0; i < moneyMovesByMonths.Count; i++)
            {
                var currentYear = Convert.ToInt32(moneyMovesByMonths[i][1]);
                double sum = 0;
                if (currentYear > lastYear)
                {
                    for (int j = i; j < moneyMovesByMonths.Count; j++)
                    {
                        var thisYear = Convert.ToInt32(moneyMovesByMonths[j][1]);
                        if (thisYear == currentYear)
                        {
                            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                            sum += double.Parse(moneyMovesByMonths[j][2], cultureInfo);
                        }
                        
                    }
                    lastYear = currentYear;
                    string[] values = new[] {Convert.ToString(currentYear), Convert.ToString(sum)};
                    moneyMoves.Add(values);
                    Console.WriteLine(values[0]);
                    Console.WriteLine(values[1]);
                }
                
            }

            return moneyMoves;
        }

        static void Output()
        {
            
        }
    }
}