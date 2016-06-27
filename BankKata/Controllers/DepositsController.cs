using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class DepositsController : Controller
    {
        private BankKataContext db = new BankKataContext();

        // GET: Deposits
        public ActionResult Index()
        {
            var lines = System.IO.File.ReadAllLines(@"C: \Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt").Where(x => x != "");

            var depositsList = new List<AccountBalance>();

            foreach (var line in lines)
            {
                var splitLines = line.Split('|');
                depositsList.Add(
                    new AccountBalance
                    {
                        Date = Convert.ToDateTime(splitLines[0]),
                        Amount = Convert.ToDecimal(splitLines[1]),
                        Balance = Convert.ToDecimal(splitLines[2])
                    });
            }

            return View(depositsList);
        }

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
            accountBalance.Date = DateTime.Now;

            var lines = System.IO.File.ReadAllLines(@"C: \Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt").Last().Split('|');

            accountBalance.Balance = lines.Any() ? Convert.ToDecimal(lines[2]) + accountBalance.Amount : accountBalance.Amount;
            
            if (ModelState.IsValid)
            {
                var deposits = string.Format("{0} | {1} | {2}", accountBalance.Date, accountBalance.Amount, accountBalance.Balance);

                using (var sw = System.IO.File.AppendText(@"C:\Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt"))
                {
                    sw.WriteLine(deposits);
                }

                //db.Deposits.Add(AccountBalance);
                //db.SaveChanges();

                return RedirectToAction("Index");
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
