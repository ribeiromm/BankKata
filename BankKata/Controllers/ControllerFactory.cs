using System;
using System.Web.Mvc;
using System.Web.Routing;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType.FullName == "BankKata.Controllers.AccountBanalceController")
            {
                
            }

            return Activator.CreateInstance(controllerType, new AccountTransactions()) as Controller;
        }
    }
}