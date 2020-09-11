using System;
using System.Collections.Generic;
using System.Security.Permissions;
using System.IO;
using System.Linq;
using System.Data;

namespace BalanceReporter
{
    public class Program
    {
        public static DataTable FromCSVtoDataTable(string fileName)
        {
            //string path = System.IO.Directory.GetCurrentDirectory();
            var path = @"C:\Users\ilyaz\RiderProjects\BalanceReporter\zenin\BalanceReporter\";
            
            DataTable table = new DataTable();
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

        public static DataTable CreateMovementTable()
        {
            DataTable movementTable = new DataTable();
            movementTable.Columns.Add("period", typeof(string));
            movementTable.Columns.Add("earned", typeof(double));
            movementTable.Columns.Add("spent", typeof(double));
            return movementTable;
        }

        public static DataTable AddAmountInMovementTable(DataTable movementTable, string period, double amount)
        {
            int index = FindIndexOfStringInColumn(movementTable, period, "period");

            if (index == -1)
            {
                movementTable.Rows.Add(period, 0.0, 0.0);
                index = FindIndexOfStringInColumn(movementTable, period, "period");
            }

            if (amount >= 0)
            {
                var earned = amount + (double) movementTable.Rows[index]["earned"];
                movementTable.Rows[index].SetField(1, earned);
            }
            else
            {
                var spent = amount + (double) movementTable.Rows[index]["spent"];
                movementTable.Rows[index].SetField(2, spent);
            }

            return movementTable;
        }

        public static DataTable CreateMonthsMovementsTable(DataTable dataTable)
        {
            string[] months = {"January","February","March","April","May","June",
                "July", "August","September","October","November","December"};

            DataTable movementTable = CreateMovementTable();

            foreach (DataRow row in dataTable.Rows)
            {
                var month = row["date"].ToString();
                month = months[Convert.ToInt32(month.Split('.')[1])-1] + " " + month.Split('.')[2];

                movementTable = AddAmountInMovementTable(movementTable, month, (double)row["amount"]);
            }

            return movementTable;
        }
        
        public static DataTable CreateYearsMovementsTable(DataTable monthsMovementTable)
        {
            DataTable yearsMovementTable = CreateMovementTable();
            
            foreach (DataRow row in monthsMovementTable.Rows)
            {
                var year = row["period"].ToString();
                year = year.Split(' ')[1];
                
                yearsMovementTable = AddAmountInMovementTable(yearsMovementTable, year, (double) row["earned"]);
                yearsMovementTable = AddAmountInMovementTable(yearsMovementTable, year, (double) row["spent"]);
                
            }
            return yearsMovementTable;
        }


        public static void FindMeansByMonths(DataTable dataTable)
        {
            
        }
        
        public static void FindMeansByYears(DataTable table)
        {
            
        }
        
        public static void FindOverallMeans(DataTable table)
        {
            
        }

        public static void PrintMovements(DataTable table)
        {
            foreach(DataRow row in table.Rows)
            {
                Console.WriteLine(row["period"]);
                Console.WriteLine("Earned: {0}", row["earned"]);
                Console.WriteLine("Spent: {0}", (double)row["spent"]*-1);
            }
        }
        
        static void Main(string[] args)
        {
            string fileName = "data.csv";
            DataTable dataTable = FromCSVtoDataTable(fileName);
            DataTable monthsMovementTable = CreateMonthsMovementsTable(dataTable);
            Console.WriteLine("Months movements:");
            PrintMovements(monthsMovementTable);
            Console.WriteLine("Years movements:");
            PrintMovements(CreateYearsMovementsTable(monthsMovementTable));
        }
    }
}