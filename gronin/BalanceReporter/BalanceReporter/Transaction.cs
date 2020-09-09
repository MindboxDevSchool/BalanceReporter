using System;
using FileHelpers;

namespace BalanceReporter
{
    [DelimitedRecord(",")]
    [IgnoreFirst()]
    public class Transaction
    {
        [FieldConverter(ConverterKind.Date, "yyyy-MM-dd")]
        public DateTime Date { get; private set; }
        public String Sender { get; private set; }

        public int Amount { get; private set; }
    }
}