using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BankKata.Investiment
{
    public interface IMarketShares
    {
        IEnumerable<ShareDetails> GetShareDetailses();
    }

    public class MarketShares : IMarketShares
    {
        public IEnumerable<ShareDetails> GetShareDetailses()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:55625/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("api/ShareDetails").Result;

                if (response.IsSuccessStatusCode)
                {
                    
                }

                return null;
            }
        }

        private async void TranslateToShareDetails(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStringAsync();
            JsonConvert.DeserializeObject<ShareDetails>(data);
        }

        public static void Response(HttpResponseMessage response)
        {
            
        }
    }
}