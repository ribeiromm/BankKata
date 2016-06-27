using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            return View(db.Withdraws.ToList());
        }


        // POST: Withdraws/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Withdraw withdraw)
        {
            withdraw.WithrawDate = DateTime.Today;

            var lines = System.IO.File.ReadAllLines(@"C: \Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt").Last().Split('|');

            withdraw.Balance = lines.Any() ? Convert.ToDecimal(lines[2]) - withdraw.Amount : 0;

            if (ModelState.IsValid)
            {
                var withdraws = string.Format("{0} | {1} | {2}", withdraw.WithrawDate, withdraw.Amount, withdraw.Balance);

                using (var sw = System.IO.File.AppendText(@"C:\Users\marior\Documents\visual studio 2015\Projects\BankKata\BankKata\Content\DepositSafe.txt"))
                {
                    sw.WriteLine(withdraws);
                }

                return RedirectToAction("Index");
            }

            return View(withdraw);
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
