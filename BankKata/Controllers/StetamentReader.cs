﻿using System.Collections.Generic;
using System.Text;
using BankKata.Models;

namespace BankKata.Controllers
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
            return GetAccountTransactions();
        }
        
        public string CreateStatement()
        {
            var statement = new StringBuilder();

            statement.AppendLine(string.Format("{0} | {1} | {2} ", "Date", "Amount", "Balance"));

            foreach (var account in GetAccountTransactions())
            {
                statement.AppendLine(account.ToStatementFormat());
            }

            return statement.ToString();
        }

        private IEnumerable<Account> GetAccountTransactions()
        {
            return _accountTransactions.GetAccountTransactions();
        }
    }
}