using System;
using System.ComponentModel;

namespace BankKata.Models
{
    public class Deposit
    {
        public int DepositId { get; set; }

        public decimal Amount { get; set; }

        [DisplayName("Deposit Date")]
        public DateTime DepositDateTime { get; set; }

        public decimal Balance { get; set; }
    }
}