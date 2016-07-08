using System.Collections.Generic;

namespace BankKata.Models
{
    public interface IStatementReader
    {
        IEnumerable<Account> ReadAccountStatement();

        string CreateStatement();
    }
}
