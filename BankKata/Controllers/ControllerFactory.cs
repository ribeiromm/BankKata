using System;
using System.IO.Abstractions;
using System.Web.Mvc;
using System.Web.Routing;
using BankKata.Investiment;
using BankKata.Models;

namespace BankKata.Controllers
{
    public class ControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
           if (controllerType.FullName == typeof(AccountBalanceController).FullName)
            {
                return Activator.CreateInstance(controllerType, new StatementReader(new AccountTransactions(new FileSystem()))) as Controller;
            }

            if (controllerType.FullName == typeof (WithdrawController).FullName)
            {
                return Activator.CreateInstance(controllerType, new AccountTransactions(new FileSystem()), new SendMessage(new NotificationStrategyFactory())) as Controller;
            }

            if (controllerType.FullName == typeof(InvestimentsController).FullName)
            {
                return Activator.CreateInstance(controllerType, new MarketShares(new Transform())) as Controller;
            }

            return Activator.CreateInstance(controllerType, new AccountTransactions(new FileSystem())) as Controller;
        }
    }
}