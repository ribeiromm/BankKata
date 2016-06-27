using System;

namespace BankKata.Models
{
    public class AccountBalance
    {
        public int AccountBalanceId { get; set; }

        public decimal Amount { get; set; }
        
        public DateTime Date { get; set; }

        public decimal Balance { get; set; }
    }
}