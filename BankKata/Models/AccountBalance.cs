using System;
using System.Linq;

namespace BankKata.Models
{
    public class AccountBalance
    {
        public int AccountBalanceId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public decimal Balance { get; set; }


        public string[] GetAccountBalance()
        {
            return System.IO.File.ReadAllLines(@"C: \Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt").Where(x => x != "").ToArray();
        }

        public void SaveTransaction(string transaction)
        {
            using (var sw = System.IO.File.AppendText(@"C:\Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt"))
            {
                sw.WriteLine(transaction);
            }
        }
    }
}