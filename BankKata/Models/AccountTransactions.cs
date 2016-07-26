using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Abstractions;
using System.Linq;

namespace BankKata.Models
{
    public class AccountTransactions : IAccountTransactions
    {

        private readonly IFileSystem _fileSystem;
        private static string SafeDepositLocation => ConfigurationManager.AppSettings["SafeDepositLocation"];

        public AccountTransactions(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public IEnumerable<Account> GetAccountTransactions()
        {
            var transactions = _fileSystem.File.ReadAllLines(SafeDepositLocation).Where(x => x != "").ToArray();

            return transactions.Select(ReadAccountBalance);
        }

        public decimal GetAccountBalance(decimal transactionAmount)
        {
            var transactions = GetAccountTransactions();

            decimal balance = 0;
            if (transactions.Any())
            {
                balance = transactions.Last().Balance;
            }

            return balance + transactionAmount;
        }

        public void SaveTransaction(string transaction)
        {
            using (var sw = _fileSystem.File.AppendText(SafeDepositLocation))
            {
                sw.WriteLine(transaction);
            }
        }

        private static Account ReadAccountBalance(string line)
        {
            var splitLines = line.Split('|');
            return new Account
            {
                Date = Convert.ToDateTime(splitLines[0]),
                Amount = Convert.ToDecimal(splitLines[1]),
                Balance = Convert.ToDecimal(splitLines[2])
            };
        }
    }
}