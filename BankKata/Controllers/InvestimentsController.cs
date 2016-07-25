using System.Collections.Generic;
using System.Web.Mvc;

namespace BankKata.Controllers
{
    public class InvestimentsController : Controller
    {
        public InvestimentsController()
        {
            
        }

        // GET: Investiments
        public ActionResult Index()
        {
            return View(new List<Models.Investiment>());
        }
    }
}