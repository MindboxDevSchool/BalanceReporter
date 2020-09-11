using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week1Task2 {
    public static class TransactionsCreator {
        public static IEnumerable<Transaction> CreateFromCsv(string csvPath) {
            return File.ReadAllLines(csvPath)
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(Transaction.Create).ToList();
        }

        public static IEnumerable<Transaction> CreateFromString(string input) {
            string[] lines = input.Split(
                new[] {"\r\n", "\r", "\n"},
                StringSplitOptions.None
            );
            return lines
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(Transaction.Create).ToList();
        }
    }
}