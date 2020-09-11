using System;
using System.Collections.Generic;
using System.IO;

namespace BalanceReporterLocal
{
    public class BalanceParser
    {
        private readonly HashSet<Transaction> _container;

        public BalanceParser()
        {
            _container = new HashSet<Transaction>();
        }

        public void ParseFromFile(FileStream inputStream)
        {
            var reader = new StreamReader(inputStream);
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (line != null)
                {
                    string[] values = line.Split(';');

                    DateTime dateTime = ConvertStringToDateTime(values[0]);
                    string source = values[1];
                    double sum = double.Parse(values[2]);

                    _container.Add(new Transaction(dateTime, source, sum));
                }
            }
        }
        public DateTime ConvertStringToDateTime(string dateString)
        {
            if (dateString.Length == 0)
            {
                throw new FormatException();
            }
            return DateTime.Parse(dateString + " 12:00:00 AM",
                System.Globalization.CultureInfo.InvariantCulture);
        }

        public HashSet<Transaction> GetTransactionCatalogue()
        {
            return _container;
        }
    }
}