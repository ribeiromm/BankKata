using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using BankKata.Controllers;
using BankKata.Models;
using NUnit.Framework;

namespace BankKata.Tests.Controllers
{
    [TestFixture]
    public class WithdrawControllerShould
    {
        private WithdrawController _controller;
        private FileSystem _fileSystem;
        private static string SafeDepositLocationDirectory => ConfigurationManager.AppSettings["SafeDepositLocationDirectory"];
        private static string SafeDepositLocationTest => ConfigurationManager.AppSettings["SafeDepositLocation"];

        [SetUp]
        public void Setup()
        {
            _fileSystem = new FileSystem();
            if (!Directory.Exists(SafeDepositLocationDirectory))
                Directory.CreateDirectory(SafeDepositLocationDirectory);

            SetTransactions();
            _controller = new WithdrawController(new AccountTransactions(_fileSystem));
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(SafeDepositLocationDirectory))
                Directory.Delete(SafeDepositLocationDirectory, true);
        }

        [Test]
        public void Whithdraw_amount_request_and_update_balance()
        {
            _controller.Create(new Account { Amount = 10 });

            var transactions = _fileSystem.File.ReadAllLines(SafeDepositLocationTest).Where(x => x != "").ToArray();

            var transactionsList = transactions.Select(ReadAccountBalance).OrderByDescending(x => x.Date);

            Assert.That(transactionsList.Select(x => x.Amount).First(), Is.EqualTo(-10));
            Assert.That(transactionsList.Select(x => x.Balance).First(), Is.EqualTo(-35));
        }

        private Account ReadAccountBalance(string line)
        {
            var splitLines = line.Split('|');
            return new Account
            {
                Date = Convert.ToDateTime(splitLines[0]),
                Amount = Convert.ToDecimal(splitLines[1]),
                Balance = Convert.ToDecimal(splitLines[2])
            };
        }

        private void SetTransactions()
        {
            var transactionBuilder = new StringBuilder();

            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(-6)} | {10} | {10} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(-5)} | {5} | {15} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(-4)} | {-4} | {11} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(-3)} | {40} | {51} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(-2)} | {4} | {55} ");
            transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(-1)} | {-10} | {45} ");

            using (var sw = _fileSystem.File.AppendText(SafeDepositLocationTest))
            {
                sw.WriteLine(transactionBuilder);
            }
        }
    }
}
