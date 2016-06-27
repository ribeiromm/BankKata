using System;

namespace BankKata.Models
{
    public class Withdraw
    {
        public int Withdrawid { get; set; }

        public DateTime WithrawDate { get; set; }

        public decimal WithrawAmount { get; set; }
    }
}