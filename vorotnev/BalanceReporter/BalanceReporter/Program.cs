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
            var moneyMovesByMonths = MoneyMovesByMonths(parsedData);
            var moneyMovesByYears = MoneyMovesByYears(moneyMovesByMonths);
            var averageMoneyByMonths = AverageMoneyByMonths(parsedData);
            var averageMoneyByYears = AverageMoneyByYears(moneyMovesByMonths);
            var maxMoneyByMonths = MaxMoneyByMonths(parsedData);
            Output(moneyMovesByMonths, moneyMovesByYears, averageMoneyByMonths, averageMoneyByYears, maxMoneyByMonths);
        }

        static void StartWarning()
        {
            Console.WriteLine("Поместите csv файл с входными данными в папку с программой под именем input.csv.");
            Console.WriteLine("Для продолжения нажмите Enter.");
            Console.ReadLine();
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

        static List<string[]> MoneyMovesByMonths(List<string[]> parsedData)
        {
            var minimalDateTime = DateTime.MinValue;
            var minimalMonth = DateTime.MinValue.Month;
            var minimalYear = DateTime.MinValue.Year;
            var lastMonth = DateTime.MinValue.Month;
            var lastYear = DateTime.MinValue.Year;
            double maxIncoming = 0;
            double maxOutcoming = 0;
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
                }
                
            }

            return moneyMoves;
        }

        static List<string[]> AverageMoneyByMonths(List<string[]> parsedData)
        {
            var minimalDateTime = DateTime.MinValue;
            var minimalMonth = DateTime.MinValue.Month;
            var minimalYear = DateTime.MinValue.Year;
            var lastMonth = DateTime.MinValue.Month;
            var lastYear = DateTime.MinValue.Year;
            List<string[]> averageMoneyByMonths = new List<string[]>();
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
                    double incoming = 0;
                    double outcoming = 0;
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
                            if (double.Parse(parsedData[j][2]) >= 0)
                                incoming += double.Parse(parsedData[j][2]);
                            if (double.Parse(parsedData[j][2]) < 0)
                                outcoming += double.Parse(parsedData[j][2]);
                            // else outcoming += double.Parse(parsedData[j][2]);
                                Thread.CurrentThread.CurrentCulture = cultureInfo;
                        }
                    }

                    incoming = incoming / parsedData.Count;
                    outcoming = outcoming / parsedData.Count;
                    string[] values = {Convert.ToString(currentMonth), Convert.ToString(currentYear), Convert.ToString(incoming), Convert.ToString(outcoming)};
                    averageMoneyByMonths.Add(values);
                }
                lastMonth = currentMonth;
                lastYear = currentYear;
            }

            return averageMoneyByMonths;
        }

        static List<string[]> AverageMoneyByYears(List<string[]> moneyMovesByMonths)
        {
            List<string[]> averageMoneyByYears = new List<string[]>();
            var lastYear = DateTime.MinValue.Year;
            for (int i = 0; i < moneyMovesByMonths.Count; i++)
            {
                var currentYear = Convert.ToInt32(moneyMovesByMonths[i][1]);
                double sum = 0;
                double incoming = 0;
                double outcoming = 0;
                if (currentYear > lastYear)
                {
                    for (int j = i; j < moneyMovesByMonths.Count; j++)
                    {
                        var thisYear = Convert.ToInt32(moneyMovesByMonths[j][1]);
                        if (thisYear == currentYear)
                        {
                            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                            if (double.Parse(moneyMovesByMonths[j][2], cultureInfo) >= 0)
                                incoming += double.Parse(moneyMovesByMonths[j][2], cultureInfo);
                            else outcoming += double.Parse(moneyMovesByMonths[j][2], cultureInfo);
                        }
                        
                    }
                    lastYear = currentYear;
                    incoming = incoming / moneyMovesByMonths.Count;
                    outcoming = outcoming / moneyMovesByMonths.Count;
                    string[] values = new[] {Convert.ToString(currentYear), Convert.ToString(incoming), Convert.ToString(outcoming)};
                    averageMoneyByYears.Add(values);
                }
            }
            return averageMoneyByYears;
        }

        static string[] MaxMoneyByMonths(List<string[]> parsedData)
        {
            var minimalDateTime = DateTime.MinValue;
            var minimalMonth = DateTime.MinValue.Month;
            var minimalYear = DateTime.MinValue.Year;
            var lastMonth = DateTime.MinValue.Month;
            var lastYear = DateTime.MinValue.Year;
            double maxIncoming = 0;
            double maxOutcoming = 0;
            string maxSender = "";
            string maxReciever = "";
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
                            if (double.Parse(parsedData[j][2]) > maxIncoming)
                            {
                                maxIncoming = double.Parse(parsedData[j][2]);
                                maxSender = parsedData[j][1];
                            }

                            if (double.Parse(parsedData[j][2]) < maxOutcoming)
                            {
                                maxOutcoming = double.Parse(parsedData[j][2]);
                                maxReciever = parsedData[j][1];
                            }
                                
                            Thread.CurrentThread.CurrentCulture = cultureInfo;
                        }
                    }
                }
                lastMonth = currentMonth;
                lastYear = currentYear;
            }

            string[] result = new[] {Convert.ToString(maxIncoming), Convert.ToString(maxOutcoming), maxSender, maxReciever};
            return result;
        }
        
        static void Output(
            List<string[]> moneyMovesByMonths, 
            List<string[]> moneyMovesByYears, 
            List<string[]> averageMoneyByMonths,
            List<string[]> averageMoneyByYears,
            string[] maxMoneyByMonths)
        {
            Console.WriteLine("Движение денежных средств по месяцам:");
            foreach (var line in moneyMovesByMonths)
            {
                Console.WriteLine($"Месяц, год: {line[0]}.{line[1]}: {line[2]}");
            }

            Console.WriteLine();
            Console.WriteLine("Движение денежных средств по годам:");
            foreach (var line in moneyMovesByYears)
            {
                Console.WriteLine($"Год: {line[0]}: {line[1]}");
            }
            
            Console.WriteLine();
            Console.WriteLine("Средний доход по месяцам:");
            foreach (var line in averageMoneyByMonths)
            {
                Console.WriteLine($"Месяц, год: {line[0]}.{line[1]}: {line[2]}");
            }
            Console.WriteLine();
            Console.WriteLine("Средний расход по месяцам:");
            foreach (var line in averageMoneyByMonths)
            {
                Console.WriteLine($"Месяц, год: {line[0]}.{line[1]}: {line[3]}");
            }
            Console.WriteLine();
            Console.WriteLine("Средний доход по годам:");
            foreach (var line in averageMoneyByYears)
            {
                Console.WriteLine($"Год: {line[0]}: {line[1]}");
            }
            Console.WriteLine();
            Console.WriteLine("Средний расход по годам:");
            foreach (var line in averageMoneyByYears)
            {
                Console.WriteLine($"Год: {line[0]}: {line[2]}");
            }
            Console.WriteLine();
            Console.WriteLine($"Максимальный доход: {maxMoneyByMonths[0]}");
            Console.WriteLine($"Максимальный расход: {maxMoneyByMonths[1]}");
            Console.WriteLine($"Больше всего денег присылает: {maxMoneyByMonths[2]}");
            Console.WriteLine($"Больше всего денег потрачено на: {maxMoneyByMonths[3]}");
        }
    }
}