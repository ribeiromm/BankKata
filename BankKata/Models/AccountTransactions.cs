using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BankKata.Models
{
    public class AccountTransactions
    {
        public int AccountBalanceId { get; set; }

        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public decimal Balance { get; set; }

        public IEnumerable<AccountTransactions> GetAccountTransactions()
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

        private static AccountTransactions ReadAccountBalance(string line)
        {
            var splitLines = line.Split('|');
            return new AccountTransactions
            {
                Date = Convert.ToDateTime(splitLines[0]),
                Amount = Convert.ToDecimal(splitLines[1]),
                Balance = Convert.ToDecimal(splitLines[2])
            };
        }
    }
}