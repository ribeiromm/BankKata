using System.Collections.Generic;
using BankKata.Models;

namespace BankKata.Controllers
{
    public interface IStatementReader
    {
        IEnumerable<Account> ReadAccountStatement();

        string CreateStetment();
    }
}