using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    public abstract class Stock
    {
        private readonly string _company;
        private int _price;
        private readonly List<IInvestor> _investors = new List<IInvestor>();

        protected Stock(string company, int price)
        {
            _company = company;
            _price = price;
        }

        public void Attach(IInvestor investor)
        {
            _investors.Add(investor);
        }

        public void Dettach(IInvestor investor)
        {
            _investors.Remove(investor);
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

        public string Company
        {
            get { return _company; }
        }
    }

    public interface IInvestor
    {
        void Update(Stock stock);
    }

    public class Investor : IInvestor
    {
        private string _name;

        public Investor(string name)
        {
            _name = name;
        }
        public void Update(Stock stock)
        {
            throw new NotImplementedException();
        }
    }
}