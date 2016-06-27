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

            var depositsList = new List<Deposit>();

            foreach (var line in lines)
            {
                var splitLines = line.Split('|');
                depositsList.Add(
                    new Deposit
                    {
                        DepositDateTime = Convert.ToDateTime(splitLines[0]),
                        Amount = Convert.ToDecimal(splitLines[1]),
                        Balance = Convert.ToDecimal(splitLines[2])
                    });
            }

            return View(depositsList);
        }

        // POST: Deposits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deposit deposit)
        {
            deposit.DepositDateTime = DateTime.Now;

            var lines = System.IO.File.ReadAllLines(@"C: \Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt").Last().Split('|');

            deposit.Balance = lines.Any() ? Convert.ToDecimal(lines[2]) + deposit.Amount : deposit.Amount;
            
            if (ModelState.IsValid)
            {
                var deposits = string.Format("{0} | {1} | {2}", deposit.DepositDateTime, deposit.Amount, deposit.Balance);

                using (var sw = System.IO.File.AppendText(@"C:\Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt"))
                {
                    sw.WriteLine(deposits);
                }

                //db.Deposits.Add(deposit);
                //db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(deposit);
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
