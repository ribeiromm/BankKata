using System.Web.Mvc;
using System.Windows.Forms;

namespace BankKata.Controllers
{
    public class AccountBalanceController : Controller
    {
        private readonly StatementReader _statementReader = new StatementReader();
        // GET: AccountTransactions
        public ActionResult Index()
        {
            return View(_statementReader.ReadAccountStatement());
        }

        public void PrintStatement()
        {
            MessageBox.Show(_statementReader.CreateStetment());
        }
    }
}