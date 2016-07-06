using System;
using System.Web.Mvc;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            IAccountTransactions accoutAccountTransactions = new AccountTransactions();
            IController controller = Activator.CreateInstance(controllerType, new[] {accoutAccountTransactions}) as Controller;

            return controller;
        } 
    }
}