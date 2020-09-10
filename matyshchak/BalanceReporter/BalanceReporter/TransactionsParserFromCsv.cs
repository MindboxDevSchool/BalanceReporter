using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace BalanceReporter
{
    public static class TransactionsParserFromCsv
    {
        public static List<Transaction> Parse(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            var records = csv
                .GetRecords<Transaction>()
                .ToList();
            return records;
        }
    }
}