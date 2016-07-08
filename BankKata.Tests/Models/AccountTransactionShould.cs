using System;
using System.Configuration;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using BankKata.Models;
using NUnit.Framework;

namespace BankKata.Tests.Models
{
    [TestFixture]
    public class AccountTransactionShould
    {
        private AccountTransactions _accountTransactions;
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
            _accountTransactions = new AccountTransactions(_fileSystem);
        }

        [TearDown]
        public void TearDown()
        {
            if (Directory.Exists(SafeDepositLocationDirectory))
                Directory.Delete(SafeDepositLocationDirectory, true);
        }


        [TestCase(1, 46)]
        [TestCase(-1, 44)]
        public void Return_the_correct_balance(decimal amount, decimal expectedBalance)
        {
            var balance = _accountTransactions.GetAccountBalance(amount);

            Assert.That(balance, Is.EqualTo(expectedBalance));
        }

        [Test]
        public void Save_the_latest_transaction()
        {
            var date = DateTime.Now.AddSeconds(7);
            _accountTransactions.SaveTransaction($"{date} | {10} | {55}" );

            var transactions = _accountTransactions.GetAccountTransactions().Last();

            Assert.That(transactions.Amount, Is.EqualTo(10));
            Assert.That(transactions.Balance, Is.EqualTo(55));
            Assert.That(transactions.Date.ToShortDateString(), Is.EqualTo(date.ToShortDateString()));
        }

        [Test]
        public void Return_account_transactions()
        {

            var transactions = _accountTransactions.GetAccountTransactions();

            Assert.That(transactions.Count(), Is.EqualTo(6));
            Assert.That(transactions.Select(x => x.Amount).First(), Is.EqualTo(10));
            Assert.That(transactions.Select(x => x.Balance).Last(), Is.EqualTo(45));

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
            
            using (var sw = _fileSystem.File.AppendText(SafeDepositLocationTest))
            {
                sw.WriteLine(transactionBuilder);
            }
        }
    }
}
