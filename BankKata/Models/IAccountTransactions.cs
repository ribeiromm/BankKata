using System.Collections.Generic;

namespace BankKata.Models
{
    public interface IAccountTransactions
    {
        IEnumerable<Account> GetAccountTransactions();

        decimal GetAccountBalance(decimal transactionAmount);

        void SaveTransaction(string transaction);
    }
}