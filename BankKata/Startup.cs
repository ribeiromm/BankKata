using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BankKata.Startup))]
namespace BankKata
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
