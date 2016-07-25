using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace BankKata.Investiment
{
    public class Transform
    {
        public static IEnumerable<ShareDetails> TranslateToShareDetails(HttpResponseMessage response)
        {
            var sahreDetails = new JavaScriptSerializer().Deserialize<List<ShareDetails>>(response.Content.ReadAsStringAsync().Result);

            foreach (var shareDetail in sahreDetails)
            {
                shareDetail.TimeRequested = DateTime.Now;
            }

            return sahreDetails;
        }
    }
}