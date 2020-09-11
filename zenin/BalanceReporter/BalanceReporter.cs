using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.IO;
using System.Linq;
using System.Data;
using System.Diagnostics.Tracing;

namespace BalanceReporter
{
    public class BalanceReporter
    {
        public static DataTable FromCSVtoDataTable(string fileName)
        {
            //string path = System.IO.Directory.GetCurrentDirectory();
            var path = @"C:\Users\ilyaz\RiderProjects\BalanceReporter\zenin\BalanceReporter\";
            
            var table = new DataTable();
            table.Columns.Add("date", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("amount", typeof(double));
            
            using (var reader = new StreamReader(path + fileName))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');
                    table.Rows.Add(values[0], values[1], Convert.ToDouble((values[2])));
                }
            }

            return table;
        }
        
        public static int FindIndexOfStringInColumn(DataTable table, string value, string column)
        {
            int index = -1;
            foreach (DataRow balanceRow in table.Rows)
            {
                if (balanceRow[column].ToString() == value)
                    index = table.Rows.IndexOf(balanceRow);
            }

            return index;
        }

        public static DataTable CreateBalanceTable()
        {
            var movementTable = new DataTable();
            movementTable.Columns.Add("period", typeof(string));
            movementTable.Columns.Add("earned", typeof(double));
            movementTable.Columns.Add("spent", typeof(double));
            movementTable.Columns.Add("positive_transactions", typeof(int));
            movementTable.Columns.Add("negative_transactions", typeof(int));
            movementTable.Columns.Add("max_earnings", typeof(double));
            movementTable.Columns.Add("max_costs", typeof(double));
            movementTable.Columns.Add("max_investor", typeof(string));
            movementTable.Columns.Add("max_spender", typeof(string));
            
            return movementTable;
        }

        public static DataTable UpdateMaximumAmounts(DataTable balanceTable, string period, double amount)
        {
            var index = FindIndexOfStringInColumn(balanceTable, period, "period");

            if (amount >= 0)
            {
                if (amount > (double) balanceTable.Rows[index]["max_earnings"])
                    balanceTable.Rows[index].SetField("max_earnings", amount);
            }
            else
            {
                if (amount < (double) balanceTable.Rows[index]["max_costs"])
                    balanceTable.Rows[index].SetField("max_costs", amount);
            }

            return balanceTable;
        }

        public static DataTable AddAmountInBalanceTable(DataTable balanceTable, string period, double amount, 
            int numberOfTransactions)
        {
            var index = FindIndexOfStringInColumn(balanceTable, period, "period");

            if (index == -1)
            {
                balanceTable.Rows.Add(period, 0.0, 0.0, 0, 0, 0.0, 0.0, "None", "None");
                index = FindIndexOfStringInColumn(balanceTable, period, "period");
            }

            if (amount >= 0)
            {
                var earned = amount + (double) balanceTable.Rows[index]["earned"];
                balanceTable.Rows[index].SetField(1, earned);
                balanceTable.Rows[index].SetField(3, 
                    (int)balanceTable.Rows[index]["positive_transactions"] + numberOfTransactions);
            }
            else
            {
                var spent = amount + (double) balanceTable.Rows[index]["spent"];
                balanceTable.Rows[index].SetField(2, spent);
                balanceTable.Rows[index].SetField(4, 
                    (int)balanceTable.Rows[index]["negative_transactions"] + numberOfTransactions);
            }

            balanceTable = UpdateMaximumAmounts(balanceTable, period, amount);

            return balanceTable;
        }

        public static DataTable CreateMonthsBalanceTable(DataTable dataTable)
        {
            string[] months = {"January","February","March","April","May","June",
                "July", "August","September","October","November","December"};

            var balanceTable = CreateBalanceTable();

            foreach (DataRow row in dataTable.Rows)
            {
                var month = row["date"].ToString();
                month = months[Convert.ToInt32(month.Split('.')[1])-1] + " " + month.Split('.')[2];

                balanceTable = AddAmountInBalanceTable(balanceTable, month, (double)row["amount"], 1);
            }

            return balanceTable;
        }
        
        public static DataTable CreateYearsBalanceTable(DataTable monthsBalanceTable)
        {
            var yearsBalanceTable = CreateBalanceTable();
            
            foreach (DataRow row in monthsBalanceTable.Rows)
            {
                var year = row["period"].ToString();
                year = year.Split(' ')[1];
                
                yearsBalanceTable = AddAmountInBalanceTable(yearsBalanceTable, year, (double) row["earned"], 
                    (int) row["positive_transactions"]);
                yearsBalanceTable = AddAmountInBalanceTable(yearsBalanceTable, year, (double) row["spent"],
                    (int) row["negative_transactions"]);
                
            }
            return yearsBalanceTable;
        }
        
        public static void FindOverallMeans(DataTable table)
        {
            
        }
        
        public static double FindMaximumAmountInRawTable(DataTable rawTable)
        {
            return 0.0;
        }

        public static void PrintReport(DataTable table)
        {
            foreach(DataRow row in table.Rows)
            {
                Console.WriteLine(row["period"]);
                Console.WriteLine("Earned: {0:F1}", row["earned"]);
                Console.WriteLine("Spent: {0:F1}", (double)row["spent"]*-1);
                
                var positiveTransactions = (int) row["positive_transactions"];
                if (positiveTransactions != 0)
                    Console.WriteLine("Average income: {0:F1}", 
                        (double)row["earned"]/positiveTransactions);
                else
                    Console.WriteLine("Average income: {0:F1}", 0);
                
                var negativeTransactions = (int) row["negative_transactions"];
                if (negativeTransactions != 0)
                    Console.WriteLine("Average consumption: {0:F1}", 
                        (double)row["spent"]/negativeTransactions * -1);
                else
                    Console.WriteLine("Average consumption: {0:F1}", 0);
                
                Console.WriteLine("Maximum earnings: {0:F1}", row["max_earnings"]);
                Console.WriteLine("Maximum costs: {0:F1}", (double)row["max_costs"]*-1);

            }
        }
        
        static void Main(string[] args)
        {
            string fileName = "data.csv";
            DataTable dataTable = FromCSVtoDataTable(fileName);
            DataTable monthsMovementsTable = CreateMonthsBalanceTable(dataTable);
            
            Console.WriteLine("Months report:");
            PrintReport(monthsMovementsTable);
            
            Console.WriteLine("Years report:");
            var yearsMovementsTable = CreateYearsBalanceTable(monthsMovementsTable);
            PrintReport(yearsMovementsTable);
        }
    }
}