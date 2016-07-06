using System.Collections.Generic;
using System.Text;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class StatementReader
    {
        private readonly AccountTransactions _accountTransactions = new AccountTransactions();
        public IEnumerable<AccountTransactions> ReadAccountStatement()
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