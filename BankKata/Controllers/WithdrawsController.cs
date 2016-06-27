using System;
using System.Linq;
using System.Web.Mvc;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class WithdrawsController : Controller
    {
        private BankKataContext db = new BankKataContext();

        // GET: Withdraws
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: Withdraws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountBalance accountBalance)
        {
            accountBalance.Date = new Clock().Now();

            var lines = accountBalance.GetAccountBalance().Last().Split('|');

            accountBalance.Balance = lines.Any() ? Convert.ToDecimal(lines[2]) - accountBalance.Amount : 0;

            if (ModelState.IsValid)
            {
                var withdraws = string.Format("{0} | {1} | {2}", accountBalance.Date, -accountBalance.Amount, accountBalance.Balance);

                using (var sw = System.IO.File.AppendText(@"C:\Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt"))
                {
                    sw.WriteLine(withdraws);
                }

                return RedirectToAction("Index", "Deposits");
            }

            return View(accountBalance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
