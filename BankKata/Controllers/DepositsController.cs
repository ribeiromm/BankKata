using System;
using System.Linq;
using System.Web.Mvc;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class DepositsController : Controller
    {
        private BankKataContext db = new BankKataContext();

        public ActionResult Create()
        {
            return View();
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountBalance accountBalance)
        {
            accountBalance.Date = new Clock().Now();

            var lines = accountBalance.GetAccountBalance().Last().Split('|');

            accountBalance.Balance = lines.Any() ? Convert.ToDecimal(lines[2]) + accountBalance.Amount : accountBalance.Amount;
            
            if (ModelState.IsValid)
            {
                var transaction = accountBalance.BuildTransaction(accountBalance.Date, accountBalance.Amount, accountBalance.Balance);

                accountBalance.SaveTransaction(transaction);

                return RedirectToAction("Index", "AccountBalance");
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