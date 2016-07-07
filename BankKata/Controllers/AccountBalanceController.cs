using System.Web.Mvc;
using System.Windows.Forms;

namespace BankKata.Controllers
{
    public class AccountBalanceController : Controller
    { 

        private readonly IStatementReader _steStatementReader;

        public AccountBalanceController(IStatementReader statementReader)
        {
            _steStatementReader = statementReader;
        }
        
        // GET: AccountTransactions
        public ActionResult Index()
        {
            return View(_steStatementReader.ReadAccountStatement());
        }

        public ActionResult DisplayStatement()
        {
            MessageBox.Show(_steStatementReader.CreateStatement());
            return RedirectToAction("Index");
        }
    }
}