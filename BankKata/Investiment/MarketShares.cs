using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BankKata.Investiment
{
    public interface IMarketShares
    {
        IEnumerable<ShareDetails> GetShareDetailses();
    }

    public class MarketShares : IMarketShares
    {
        private readonly ITransform _transform;

        public MarketShares(ITransform transform)
        {
            _transform = transform;
        }

        public static string BaseWebApiUri => ConfigurationManager.AppSettings["BaseWebApiUri"];

        public IEnumerable<ShareDetails> GetShareDetailses()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseWebApiUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/ShareDetails").Result;

                if (response.IsSuccessStatusCode)
                {
                    return _transform.TranslateToShareDetails(response);
                }

                return null;
            }
        }
    }
}