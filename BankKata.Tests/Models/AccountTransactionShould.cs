using System;
using System.Configuration;
using System.IO;
using System.Text;
using BankKata.Models;
using NUnit.Framework;

namespace BankKata.Tests.Models
{
    [TestFixture]
    public class AccountTransactionShould
    {
        private string SafeDepositLocationTest => ConfigurationManager.AppSettings["SafeDepositLocationTest"];

        [SetUp]
        public void Setup()
        {
            var test = Directory.Exists(SafeDepositLocationTest);
            if (!Directory.Exists(SafeDepositLocationTest))
                Directory.CreateDirectory(SafeDepositLocationTest);

            var transactionBuilder = new StringBuilder();
            transactionBuilder.Append($"{DateTime.Now} | {10} | {10} ");

            using (var sw = File.AppendText(SafeDepositLocationTest))
            {
                sw.WriteLine(transactionBuilder);
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(SafeDepositLocationTest))
                Directory.Delete(SafeDepositLocationTest, true);
        }


        [TestCase(1, 11)]
        public void Returnbalance(decimal amount, decimal expectedBalance)
        {
            var balance = new AccountTransactions().GetAccountBalance(amount);

            Assert.That(balance, Is.EqualTo(11));
        }
    }
}
