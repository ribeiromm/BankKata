using System;
using System.ComponentModel.DataAnnotations;

namespace BankKata.Models
{
    public class Account
    {
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public decimal Balance { get; set; }

        public string ToStatementFormat()
        {
            return $"{Date.ToShortDateString()} | {Amount} | {Balance} ";
        }
    }
}