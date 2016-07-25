using System.Collections.Generic;
using System.Web.Mvc;
using BankKata.Investiment;

namespace BankKata.Controllers
{
    public class InvestimentsController : Controller
    {
        private readonly IMarketShares _marketShares;

        public InvestimentsController(IMarketShares marketShares)
        {
            _marketShares = marketShares;
        }

        // GET: Investiments
        public ActionResult Index()
        {
            var test = _marketShares.GetShareDetailses();
            return View(new List<ShareDetails>());
        }
    }
}