using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.IO;
using System.Linq;

namespace BalanceReporter
{
    public class Program
    {
        public static int FromCSVtoLists(string fileName)
        {
            List<string> dateList = new List<string>();
            List<string> nameList = new List<string>();
            List<double> moneyList = new List<double>();
            //string path = System.IO.Directory.GetCurrentDirectory();
            var path = @"C:\Users\ilyaz\RiderProjects\BalanceReporter\zenin\BalanceReporter\";
            
            using (var reader = new StreamReader(path + fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    dateList.Add(values[0]);
                    nameList.Add(values[1]);
                    moneyList.Add(Convert.ToDouble((values[2])));
                }
            }

            MonthsBalance(dateList, nameList, moneyList);
            return dateList.Capacity;
        }

        public static int MonthsBalance(List<string> dateList, List<string> nameList, List<double> moneyList)
        {
            List<string> months = new List<string>();
            List<double> earned = new List<double>();
            List<double> spent = new List<double>();

            foreach (string record in dateList)
            {
                var month = record.Split('.')[1] + " " + record.Split('.')[2];
                if (!months.Contains(month))
                {
                    //Console.WriteLine(month);
                    months.Add(month);
                    earned.Add(0.0);
                    spent.Add(0.0);
                }

                var index = months.IndexOf(month);
                //Console.WriteLine(index);
                var money = moneyList[dateList.IndexOf(record)];
                if (money >= 0)
                    earned[index] += money;
                else
                    spent[index] += money;
            }

            PrintBalance(months, earned, spent);
            return months.Capacity;
        }

        public static void PrintBalance(List<string> periods, List<double> earned, List<double> spent)
        {
            foreach (string period in periods)
            {
                var index = periods.IndexOf(period);
                Console.WriteLine("{0} {1} {2}", period, earned[index], spent[index]);
            }
        }
        
        static void Main(string[] args)
        {
            string fileName = "data.csv";
            FromCSVtoLists(fileName);
            //Console.WriteLine(FromCSVtoLists(fileName));
        }
    }
}