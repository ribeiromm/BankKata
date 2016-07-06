using System;
using System.Collections.Generic;
using System.Linq;

namespace BankKata.Models
{
    public interface IAccountTransactions
    {
        IEnumerable<Account> GetAccountTransactions();
        decimal GetAccountBalance(decimal transactionAmount);
        void SaveTransaction(string transaction);
    }

    public class AccountTransactions : IAccountTransactions
    {
        public IEnumerable<Account> GetAccountTransactions()
        {
            var transactions = System.IO.File.ReadAllLines(@"C:\Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt").Where(x => x != "").ToArray();

            return transactions.Select(ReadAccountBalance).OrderByDescending(x => x.Date);
        }

        public decimal GetAccountBalance(decimal transactionAmount)
        {
            var balance = GetAccountTransactions().First();

            return balance.Balance + transactionAmount;
        }

        public void SaveTransaction(string transaction)
        {
            using (var sw = System.IO.File.AppendText(@"C:\Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt"))
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