using System.Web.Mvc;
using System.Windows.Forms;

namespace BankKata.Controllers
{
	public class AccountBalanceController : Controller
	{
	    private readonly IStatementReader _statementReader;

	    public AccountBalanceController(IStatementReader statementReader)
	    {
	        _statementReader = statementReader;

	    }
        public ActionResult Create()
        {
            return View("Index");
        }

	    public ActionResult DisplayStatement()
	    {
	        MessageBox.Show(_statementReader.CreateStatement());
	        return RedirectToAction("Create");
	    }
    }
}