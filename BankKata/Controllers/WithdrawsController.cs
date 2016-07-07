using System.Web.Mvc;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class WithdrawsController : Controller
    {
        private readonly IAccountTransactions _accountTransactions;

        public WithdrawsController(IAccountTransactions accountTransactions)
        {
            _accountTransactions = accountTransactions;
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
        public ActionResult Create(Account account)
        {
            account.Date = new Clock().Now();

            account.Balance = _accountTransactions.GetAccountBalance(account.Amount);

            if (ModelState.IsValid)
            {
                var transaction =  string.Format("{0} | {1} | {2}",account.Date, -account.Amount, account.Balance);

                _accountTransactions.SaveTransaction(transaction);

                return RedirectToAction("Index", "AccountBalance");
            }

            return View(account);
        }
    }
}
