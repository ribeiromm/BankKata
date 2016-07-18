using System;
using System.Web.Script.Serialization;

namespace BankKata.Investiment
{
    public class MarketShares
    {
        private void SharesPrice()
        {
           var random = new Random();

            var price = 1;
            while (price >= 1 && price < 5)
            {
                price = random.Next(2, 10);
            }

            var shares = new Share { Company = "Corp Ltd", Price = price};

            var json = new JavaScriptSerializer().Serialize(shares);
        }
    }

    internal class Share
    {
        public string Company { get; set; }
        public int Price { get; set; }
    }
}