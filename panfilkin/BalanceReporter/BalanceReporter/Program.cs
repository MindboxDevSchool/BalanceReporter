using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BalanceReporter
{
    public static class Program
    {
        private static void Main()
        {
            var bankData = LoadCsvWithHeaders("bankData.csv");
            PrintBankDataStatistics(bankData);
        }

        public static List<Dictionary<string, dynamic>> LoadCsvWithHeaders(string pathToFile)
        {
            var csvData = new List<Dictionary<string, dynamic>>();
            if (File.Exists(pathToFile))
            {
                using var streamReader = new StreamReader(pathToFile);
                string readiedLine;
                var csvFileHeaders = CsvStringParser(streamReader.ReadLine());
                while ((readiedLine = streamReader.ReadLine()) != null)
                {
                    var parsedLineList = CsvStringParser(readiedLine);
                    if (parsedLineList.Count == csvFileHeaders.Count)
                    {
                        var dataRow = new Dictionary<string, dynamic>();
                        for (var i = 0; i < parsedLineList.Count; i++)
                            dataRow.Add(csvFileHeaders[i], parsedLineList[i]);

                        csvData.Add(dataRow);
                    }
                    else
                    {
                        throw new Exception("The amount of data per line and heading differ!");
                    }
                }
            }
            else
            {
                throw new Exception("File not found!");
            }

            return csvData;
        }

        public static List<string> CsvStringParser(string stringLine, string separator = ",")
        {
            var parsedLineList = new List<string>();
            while (stringLine.Length != 0)
            {
                var separatorIndex = stringLine.IndexOf(separator, StringComparison.Ordinal);
                if (separatorIndex != -1)
                {
                    parsedLineList.Add(stringLine.Substring(0, separatorIndex).Trim());
                    stringLine = stringLine.Remove(0, separatorIndex + 1);
                }
                else
                {
                    parsedLineList.Add(stringLine.Substring(0, stringLine.Length).Trim());
                    stringLine = stringLine.Remove(0, stringLine.Length);
                }
            }

            return parsedLineList;
        }

        public static void PrintBankDataStatistics(List<Dictionary<string, dynamic>> bankData)
        {
            var yearMonthFundsData =
                CalculateStatisticsOfMovementOfFundsByMonths(bankData);
            var yearFundsData = CalculateStatisticsOfMovementOfFundsByYears(bankData);

            var averageIncome = CalculateAverageIncome(bankData);
            var averageExpense = CalculateAverageExpense(bankData);

            var maximumIncome = CalculateMaximumIncome(bankData);
            var maximumExpense = CalculateMaximumExpense(bankData);

            var nameWhoMaxSend = CalculateNameWhoMaxSend(bankData);
            var nameWhoMaxGet = CalculateNameWhoMaxGet(bankData);
        }

        public static Dictionary<int, Dictionary<int, double>> CalculateStatisticsOfMovementOfFundsByMonths(
            List<Dictionary<string, dynamic>> bankData)
        {
            var dataSortedByDate = bankData.OrderBy(column => DateTime.Parse(column["date"], CultureInfo.InvariantCulture)).ToList();
            var monthFundsDataByYear = new Dictionary<int, Dictionary<int, double>>();
            foreach (var dataRow in dataSortedByDate)
            {
                DateTime date = DateTime.Parse(dataRow["date"], CultureInfo.InvariantCulture);
                if (!monthFundsDataByYear.ContainsKey(date.Year))
                {
                    monthFundsDataByYear[date.Year] = new Dictionary<int, double>();
                    for (var month = 1; month <= 12; month++) monthFundsDataByYear[date.Year][month] = 0;
                }

                monthFundsDataByYear[date.Year][date.Month] +=
                    double.Parse(dataRow["amount"].ToString(), CultureInfo.InvariantCulture);
            }

            return monthFundsDataByYear;
        }

        public static Dictionary<int, double> CalculateStatisticsOfMovementOfFundsByYears(
            List<Dictionary<string, dynamic>> bankData)
        {
            var yearFundsData = new Dictionary<int, double>();
            var monthFundsDataByYear = CalculateStatisticsOfMovementOfFundsByMonths(bankData);
            foreach (var yearRow in monthFundsDataByYear)
            {
                if (!yearFundsData.ContainsKey(yearRow.Key)) yearFundsData[yearRow.Key] = 0;

                for (var m = 1; m <= 12; m++) yearFundsData[yearRow.Key] += yearRow.Value[m];
            }

            return yearFundsData;
        }

        public static double CalculateAverageIncome(List<Dictionary<string, dynamic>> bankData)
        {
            double totalIncomes = 0;
            var countOfIncomes = 0;
            foreach (var dataRow in bankData)
            {
                var income = double.Parse(dataRow["amount"].ToString(), CultureInfo.InvariantCulture);
                if (income < 0) continue;
                totalIncomes += income;
                countOfIncomes++;
            }

            var averageIncome = totalIncomes / countOfIncomes;
            return averageIncome;
        }

        public static double CalculateAverageExpense(List<Dictionary<string, dynamic>> bankData)
        {
            double totalExpenses = 0;
            var countOfExpenses = 0;
            foreach (var dataRow in bankData)
            {
                var expense = double.Parse(dataRow["amount"].ToString(), CultureInfo.InvariantCulture);
                if (expense < 0)
                {
                    totalExpenses += expense;
                    countOfExpenses++;
                }
            }

            var averageExpense = totalExpenses / countOfExpenses;
            return averageExpense;
        }

        public static double CalculateMaximumIncome(List<Dictionary<string, dynamic>> bankData)
        {
            var maximumIncome = double.Parse(bankData[0]["amount"], CultureInfo.InvariantCulture);
            foreach (var row in bankData)
            {
                double income = double.Parse(row["amount"], CultureInfo.InvariantCulture);
                if (income > maximumIncome) maximumIncome = income;
            }

            return maximumIncome;
        }

        public static double CalculateMaximumExpense(List<Dictionary<string, dynamic>> bankData)
        {
            var maximumExpense = double.Parse(bankData[0]["amount"], CultureInfo.InvariantCulture);
            foreach (var row in bankData)
            {
                double expense = double.Parse(row["amount"], CultureInfo.InvariantCulture);
                if (expense < maximumExpense) maximumExpense = expense;
            }

            return maximumExpense;
        }

        public static string CalculateNameWhoMaxSend(List<Dictionary<string, dynamic>> bankData)
        {
            var sendersSend = new Dictionary<string, double>();
            foreach (var dataRow in bankData)
            {
                var incoming = double.Parse(dataRow["amount"].ToString(), CultureInfo.InvariantCulture);
                if (incoming > 0)
                {
                    if (!sendersSend.ContainsKey(dataRow["subject"])) sendersSend[dataRow["subject"]] = 0;

                    sendersSend[dataRow["subject"]] += incoming;
                }
            }

            var sendersSendSorted = sendersSend.OrderByDescending(sender => sender.Value).First();
            return sendersSendSorted.Key;
        }

        public static string CalculateNameWhoMaxGet(List<Dictionary<string, dynamic>> bankData)
        {
            var sendersGet = new Dictionary<string, double>();
            foreach (var dataRow in bankData)
            {
                var expense = double.Parse(dataRow["amount"].ToString(), CultureInfo.InvariantCulture);
                if (expense < 0)
                {
                    if (!sendersGet.ContainsKey(dataRow["subject"])) sendersGet[dataRow["subject"]] = 0;

                    sendersGet[dataRow["subject"]] += expense;
                }
            }

            var sendersGetSorted = sendersGet.OrderBy(sender => sender.Value).First();
            return sendersGetSorted.Key;
        }
    }
}