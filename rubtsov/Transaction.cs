using System;

namespace BankAccounts
{
    public class Transaction
    {
        public DateTime Date { get; private set; }
        public string CompanyName { get; private set; }
        public double Sum { get; private set; }

        public Transaction(DateTime date, string companyName, double sum)
        {
            Date = date;
            CompanyName = companyName;
            Sum = sum;
        }
    }
}