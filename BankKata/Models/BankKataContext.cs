using System.Data.Entity;

namespace BankKata.Models
{
    public class BankKataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BankKataContext() : base("name=BankKataContext")
        {
        }

        public DbSet<Deposit> Deposits { get; set; }

        public System.Data.Entity.DbSet<BankKata.Models.Withdraw> Withdraws { get; set; }
    }
}
