using System;
using System.ComponentModel;

namespace BankKata.Models
{
    public class Withdraw
    {
        public int Withdrawid { get; set; }

        public DateTime WithrawDate { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }
}