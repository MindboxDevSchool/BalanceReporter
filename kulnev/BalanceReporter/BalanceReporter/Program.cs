using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration.Attributes;


namespace BalanceReporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Balance t=new Balance();
            t.ReadTransactionsFromCsvFile("C:/Users/Linonse/Desktop/BalanceReporter/BalanceReporter/kulnev/BalanceReporter/bankStatementsTask2_v3.csv");
            t.WriteTheListOfTransaction();
        }
    }

    public class Balance
    {
        public class Transaction
        {
            [Index(0)] public DateTime Date { get; set; }
            [Index(1)] public string Receiver { get; set; }
            [Index(2)] public double Total { get; set; }

            public Transaction(DateTime date, string receiver, double total)
            {
                Date = date;
                Receiver = receiver;
                Total = total;
            }
        }
        
        private List<Transaction> _transactions;
        
        public void ReadTransactionsFromCsvFile(string filename)
        {
            using (StreamReader streamReader = new StreamReader(filename))
            {
                using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Configuration.Delimiter = ",";
                    csvReader.Configuration.HasHeaderRecord = false;
                    IEnumerable<Transaction> records = csvReader.GetRecords<Transaction>();
                    _transactions = records.ToList();
                }
            }
        }

        public void WriteTheListOfTransaction()
        {
            foreach (var i in _transactions)
            {
                Console.WriteLine(i.Date);
            }
        }
        
    }
}