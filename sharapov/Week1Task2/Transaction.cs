using System;
using System.Globalization;

namespace Week1Task2 {
    public class Transaction {
        public DateTime Date { get; set; } // Дата
        public string VendorName { get; set; } // С кем была транзакция
        public double Value { get; set; } // Сумма транзакции 
        public Type TransactionType { get; set; } // Income or Outcome

        private const string OutputDateFormat = "MM/dd/yyyy";

        public enum Type {
            Income,
            Outcome
        }

        public static Transaction Create(string row) {
            var splintedRow = row.Split(";");
            var (value, isIncome) = ParseValue(splintedRow[2]);
            var transaction = new Transaction {
                Date = DateTime.Parse(splintedRow[0]),
                VendorName = splintedRow[1],
                Value = value,
                TransactionType = isIncome
            };
            return transaction;
        }

        public override string ToString() {
            string outputValue;
            if (TransactionType == Type.Income) {
                outputValue = $"+{Value}";
            }
            else {
                outputValue = Math.Abs(Value).ToString(CultureInfo.CurrentCulture);
            }

            return $"{Date.ToString(OutputDateFormat)} {VendorName} {outputValue}";
        }

        private static (double, Type) ParseValue(string value) {
            double result;
            Type type;
            if (value[0] == '+') { // Cut '+' and cast to double.  +111.222 -> 111.222 sign = Sign.Positive
                var valueWithoutPlus = value.Substring(1);
                result = double.Parse(valueWithoutPlus);
                type = Type.Income;
            }
            else { // Cast to double. -111.222 -> 111.222 sign = Sign.Negative
                result = double.Parse(value);
                type = Type.Outcome;
            }

            return (result, type);
        }

        protected bool Equals(Transaction other) {
            return Date.Equals(other.Date)
                   && VendorName == other.VendorName
                   && Value.Equals(other.Value)
                   && TransactionType == other.TransactionType;
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Transaction) obj);
        }

        //TODO
        /* 
         *     You can compute the hash code from fields that are not mutable; 
               You can ensure that the hash code of a mutable object does not change while the object is contained in a collection that relies on its hash code. 
         */
        public override int GetHashCode() {
            return HashCode.Combine(Date, VendorName, Value, (int) TransactionType);
        }
    }
}