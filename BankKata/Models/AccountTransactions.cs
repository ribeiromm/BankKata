using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace BankKata.Models
{
    public class AccountTransactions : IAccountTransactions
    {
        private static string SafeDepositLocation => ConfigurationManager.AppSettings["SafeDepositLocation"];

        public IEnumerable<Account> GetAccountTransactions()
        {
            var transactions = System.IO.File.ReadAllLines(SafeDepositLocation).Where(x => x != "").ToArray();

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
            using (var sw = System.IO.File.AppendText(SafeDepositLocation))
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