using System.Web.Mvc;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class WithdrawController : Controller
    {
        private readonly IAccountTransactions _accountTransactions;
        private ISendMessage _sendMessage;

        public WithdrawController(IAccountTransactions accountTransactions, ISendMessage sendMessage)
        {
            _accountTransactions = accountTransactions;
            _sendMessage = sendMessage;
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

            account.Balance = _accountTransactions.GetAccountBalance(-account.Amount);

            if (ModelState.IsValid)
            {
                var transaction =  account.ToStatementFormat();

                _accountTransactions.SaveTransaction(transaction);

                _sendMessage.Send(account);

                return RedirectToAction("Index", "AccountBalance");
            }

            return View(account);
        }
    }
}
