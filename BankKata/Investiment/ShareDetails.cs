using System;
using System.ComponentModel;

namespace BankKata.Investiment
{
    public class ShareDetails
    { 
        public string CompanyName { get; set; }

        [DisplayName("Share Price")]
        public string Price { get; set; }

        public DateTime Date { get; set; }

        public DateTime TimeRequested { get; set; }
    }
}