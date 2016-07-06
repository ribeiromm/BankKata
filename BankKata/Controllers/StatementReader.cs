using System.Collections.Generic;
using System.Text;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class StatementReader
    {
        private readonly IAccountTransactions _accountTransactions;

        public StatementReader(IAccountTransactions accountTransactions)
        {
            _accountTransactions = accountTransactions;
        }

        public IEnumerable<Account> ReadAccountStatement()
        {
            return _accountTransactions.GetAccountTransactions();
        }

        public string CreateStetment()
        {
            var statement = new StringBuilder();

            statement.AppendLine(string.Format("{0} | {1} | {2} ", "Date", "Amount", "Balance"));

            foreach (var line in _accountTransactions.GetAccountTransactions())
            {
                statement.AppendLine(string.Format("{0} | {1} | {2} ", line.Date.ToShortDateString(), line.Amount, line.Balance));
            }

            return statement.ToString();
        }
    }
}