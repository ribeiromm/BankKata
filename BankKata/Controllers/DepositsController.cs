﻿using System;
using System.Linq;
using System.Web.Mvc;
using BankKata.Models;
using Microsoft.Ajax.Utilities;

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
        public ActionResult Create(AccountTransactions accountTransactions)
        {
            accountTransactions.Date = new Clock().Now();
            
            accountTransactions.Balance = accountTransactions.GetAccountBalance(accountTransactions.Amount);
            
            if (ModelState.IsValid)
            {
                var transaction = string.Format("{0} | {1} | {2} ", accountTransactions.Date, accountTransactions.Amount, accountTransactions.Balance);

                accountTransactions.SaveTransaction(transaction);

                return RedirectToAction("Index", "AccountBalance");
            }

            return View(accountTransactions);
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