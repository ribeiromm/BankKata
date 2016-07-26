using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace BankKata.Investiment
{
    public class Transform : ITransform
    {
        public IEnumerable<ShareDetails> TranslateToShareDetails(HttpResponseMessage response)
        {
            var shareDetails = new JavaScriptSerializer().Deserialize<List<ShareDetails>>(response.Content.ReadAsStringAsync().Result);

            foreach (var shareDetail in shareDetails)
            {
                shareDetail.TimeRequested = DateTime.Now;
            }

            return shareDetails;
        }
    }
}