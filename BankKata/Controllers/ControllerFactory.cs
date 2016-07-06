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
            IController controller = Activator.CreateInstance(controllerType, new AccountTransactions()) as Controller;

            return controller;
        } 
    }
}