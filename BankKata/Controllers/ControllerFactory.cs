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
            if (controllerType.FullName == "BankKata.Controllers.AccountBalanceController")
            {
                return Activator.CreateInstance(controllerType, null) as Controller;
            }

            return Activator.CreateInstance(controllerType, new AccountTransactions()) as Controller;
        } 
    }
}