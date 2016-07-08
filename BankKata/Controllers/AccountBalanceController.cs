using System.Linq;
using System.Web.Mvc;
using System.Windows.Forms;
using BankKata.Models;

namespace BankKata.Controllers
{
	public class AccountBalanceController : Controller
	{
	    private readonly IStatementReader _statementReader;

	    public AccountBalanceController(IStatementReader statementReader)
	    {
	        _statementReader = statementReader;

	    }
        public ActionResult Index()
        {
            return View(_statementReader.ReadAccountStatement());
        }

	    public ActionResult DisplayStatement()
	    {
	        MessageBox.Show(_statementReader.CreateStatement());
	        return RedirectToAction("Index");
	    }
    }
}