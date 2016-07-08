using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankKata.Controllers;
using BankKata.Models;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BankKata.Tests.Controllers
{
    [TestFixture]
    public class AccountBalanceControllerShould
    {
        private AccountBalanceController _controller;
        private static string SafeDepositLocationDirectory => ConfigurationManager.AppSettings["SafeDepositLocationDirectory"];
        private static string SafeDepositLocationTest => ConfigurationManager.AppSettings["SafeDepositLocation"];

        [SetUp]
        public void Setup()
        {
            if (!Directory.Exists(SafeDepositLocationDirectory))
                Directory.CreateDirectory(SafeDepositLocationDirectory);

            SetTransactions();
            _controller = new AccountBalanceController(new StatementReader(new AccountTransactions()));
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(SafeDepositLocationDirectory))
                Directory.Delete(SafeDepositLocationDirectory, true);
        }

        [Test]
        public void Return_view_with_statement_balance()
        {
            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result.Model);
        }

        [Test]
        public void Return_view_with_print_statement()
        {
            
        }

        private void SetTransactions()
        {
            var transactionBuilder = new StringBuilder();

            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(1)} | {10} | {10} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(2)} | {5} | {15} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(3)} | {-4} | {11} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(4)} | {40} | {51} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(5)} | {4} | {55} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(6)} | {-10} | {45} ");

            using (var sw = File.AppendText(SafeDepositLocationTest))
            {
                sw.WriteLine(transactionBuilder);
            }
        }
    }
}
