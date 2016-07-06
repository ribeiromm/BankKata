using System.Web.Mvc;
using System.Windows.Forms;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class AccountBalanceController : Controller
    {
        private readonly StatementReader _statementReader = new StatementReader(new AccountTransactions());
        private readonly IAccountTransactions _accounTransactions;

        public AccountBalanceController(IAccountTransactions accountTransactions)
        {
            _accounTransactions = accountTransactions;
        }

        // GET: AccountTransactions
        public ActionResult Index()
        {
            return View(_statementReader.ReadAccountStatement());
        }

        public ActionResult DisplayStatement()
        {
            MessageBox.Show(_statementReader.CreateStetment());
            return RedirectToAction("Index");
        }
    }
}