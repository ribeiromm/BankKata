using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        // GET: Deposits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // GET: Deposits/Create
        public ActionResult Create()
        {
            return View();
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

        // GET: Deposits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // POST: Deposits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Deposit deposit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(deposit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(deposit);
        }

        // GET: Deposits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deposit deposit = db.Deposits.Find(id);
            if (deposit == null)
            {
                return HttpNotFound();
            }
            return View(deposit);
        }

        // POST: Deposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deposit deposit = db.Deposits.Find(id);
            db.Deposits.Remove(deposit);
            db.SaveChanges();
            return RedirectToAction("Index");
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
