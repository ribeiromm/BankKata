using System;

namespace BankKata.Models
{
    public class Investiment
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string SharePrice { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeRequested { get; set; }
    }
}