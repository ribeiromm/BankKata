using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BankKata.Controllers;

namespace BankKata
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterControlleerFacory();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void RegisterControlleerFacory()
        {
            IControllerFactory controllerFactory = new ControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
