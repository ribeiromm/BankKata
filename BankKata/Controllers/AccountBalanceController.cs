using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web.Mvc;
using System.Windows.Forms;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class AccountBalanceController : Controller
    {
        private string[] _statement;
        
        // GET: AccountBalance
        public ActionResult Index()
        {
            var accountStatement = new List<AccountBalance>();

            foreach (var line in Statement)
            {
                var splitLines = line.Split('|');
                accountStatement.Add(
                    new AccountBalance
                    {
                        Date = Convert.ToDateTime(splitLines[0]),
                        Amount = Convert.ToDecimal(splitLines[1]),
                        Balance = Convert.ToDecimal(splitLines[2])
                    });
            }

            return View(accountStatement);
        }

        public void PrintStatement()
        {
            var statement = new StringBuilder();
            statement.AppendLine(new AccountBalance().BuildTransaction("Date", "Amount", "Balance"));

            foreach(var line in Statement)
            {
                statement.AppendLine(line);
            }

            MessageBox.Show(statement.ToString());

            RedirectToAction("Index");
        }

        public string[] Statement
        {
            get
            {
                if(_statement != null)
                    return _statement;

                return _statement = new AccountBalance().GetAccountBalance();
            }
        }
    }
}