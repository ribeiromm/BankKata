using System.Collections.Generic;
using System.Linq;
using System.Text;
using BankKata.Controllers;

namespace BankKata.Models
{
    public class StatementReader : IStatementReader
    {
        private readonly IAccountTransactions _accountTransactions;

        public StatementReader(IAccountTransactions accountTransactions)
        {
            _accountTransactions = accountTransactions;
        }

        public IEnumerable<Account> ReadAccountStatement()
        {
            return GetAccountTransactions().OrderByDescending(x => x.Date);
        }
        
        public string CreateStatement()
        {
            var statement = new StringBuilder();

            statement.AppendLine($"{"Date"} | {"Amount"} | {"Balance"} ");

            foreach (var account in GetAccountTransactions().OrderByDescending(x => x.Date))
            {
                statement.AppendLine(account.ToPrintStatementFormat());
            }

            return statement.ToString();
        }

        private IEnumerable<Account> GetAccountTransactions()
        {
            return _accountTransactions.GetAccountTransactions();
        }
    }
}