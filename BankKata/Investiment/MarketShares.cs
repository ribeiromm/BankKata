using System;
using System.Collections.Generic;
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

    public class Share
    {
        public string Company { get; set; }
        public int Price { get; set; }
    }

    public abstract class StockMarket
    {
        private int _price;
        private readonly List<IPotencialInvestor> _investors = new List<IPotencialInvestor>();

        protected StockMarket(string company, int price)
        {
            Company = company;
            _price = price;
        }

        public void Attach(IPotencialInvestor potencialInvestor)
        {
            _investors.Add(potencialInvestor);
        }

        public void Dettach(IPotencialInvestor potencialInvestor)
        {
            _investors.Remove(potencialInvestor);
        }

        public void Notify()
        {
            foreach (var investor in _investors)
            {
                investor.Update(this);
            }
        }

        public int Price
        {
            get { return _price; }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    Notify();
                }
            }
        }

        public string Company { get; }
    }

    public class Company : StockMarket
    {
        public Company(string company, int price) : base(company, price)
        {
        }
    }

    public interface IPotencialInvestor
    {
        void Update(StockMarket stockMarket);
    }

    public class PotencialInvestor : IPotencialInvestor
    {
        private string _name;

        public PotencialInvestor(string name)
        {
            _name = name;
        }

        public void Update(StockMarket stockMarket)
        {
            throw new NotImplementedException();
        }

        public StockMarket StockMarket { get; set; }
    }
}