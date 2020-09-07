using System.Collections;
using System.Collections.Generic;

namespace BankAccounts
{
    public interface IDataLoader
    {
        IEnumerable<Transaction> LoadTransactions { get; }
    }
}