using System;

namespace BalanceReporter
{
    public class Transaction
    {
        public DateTime Date { get; }
        public string TransactionWithAccount;
        public Money Amount { get; }
        
        public Transaction(DateTime date, string transactionWithAccount, Money amount)
        {
            Date = date;
            TransactionWithAccount = transactionWithAccount;
            Amount = amount;
        }

    }

    public class Money
    {
        #region CTOR's
        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.SelectedCurrency = currency;
        }
        public Money(int amount, Currency currency)
            : this(Convert.ToDecimal(amount), currency)
        {
        }
        public Money(double amount, Currency currency)
            : this(Convert.ToDecimal(amount), currency)
        {
        }
        #endregion
        public decimal Amount { get; set; }
        public Currency SelectedCurrency { get; set; }
        
        #region Ariphmetical operations
        public static Money operator +(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount + secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator -(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount - secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator *(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount * secondValue.Amount, firstValue.SelectedCurrency);
        }

        public static Money operator /(Money firstValue, Money secondValue)
        {
            if (firstValue.SelectedCurrency != secondValue.SelectedCurrency)
            {
                throw new InvalidCastException("Calculation is using different currencies!");
            }

            return new Money(firstValue.Amount / secondValue.Amount, firstValue.SelectedCurrency);
        }

        #endregion
    }

    /// <summary>
    /// 3-symb representation of currency
    /// </summary>
    public enum Currency
    {
        RUR, USD, EUR
    }
}