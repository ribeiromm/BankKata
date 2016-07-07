using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankKata.Models
{
    public class Account
    {
        public decimal Amount { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy")]
        public DateTime Date { get; set; }

        public decimal Balance { get; set; }
    }
}