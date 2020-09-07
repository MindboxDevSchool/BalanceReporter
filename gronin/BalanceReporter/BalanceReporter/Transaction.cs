using System;
using FileHelpers;

namespace BalanceReporter
{
    [DelimitedRecord(",")]
    public class Transaction
    {
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        [FieldOrder(10)]
        public DateTime Date;

        [FieldOrder(20)]
        public String Sender;
        
        [FieldOrder(30)]
        public decimal Amount;
    }
}