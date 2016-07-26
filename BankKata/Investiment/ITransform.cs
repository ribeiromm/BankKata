using System.Collections.Generic;
using System.Net.Http;

namespace BankKata.Investiment
{
    public interface ITransform
    {
        IEnumerable<ShareDetails> TranslateToShareDetails(HttpResponseMessage response);
    }
}