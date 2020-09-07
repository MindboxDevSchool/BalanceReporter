using System;
using System.Collections.Generic;
using System.IO;

namespace BankAccounts
{
    public class CsvDataLoader : IDataLoader
    {
        private IEnumerable<Transaction> Transactions { get; }
        public IEnumerable<Transaction> LoadTransactions => Transactions;

        public CsvDataLoader(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException("The file does not exist.");

            using var reader = new StreamReader(filename);
            List<Transaction> transactions = new List<Transaction>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line?.Split(';');
                transactions.Add(new Transaction(Convert.ToDateTime(values?[0]), values?[1], Convert.ToDouble(values?[2])));
            }
            Transactions = transactions;
        }

        
    }
}