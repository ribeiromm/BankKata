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
    public class StetamentReaderShould
    {
        private StatementReader _statementReader;
        private static StringBuilder _transactionBuilder;
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
            _statementReader = new StatementReader(new AccountTransactions(_fileSystem));
        }

        [TearDownAttribute]
        public void TearDown()
        {
            if (Directory.Exists(SafeDepositLocationDirectory))
                Directory.Delete(SafeDepositLocationDirectory, true);
        }

        [Test]
        public void Read_account_stetament()
        {
            var statement = _statementReader.ReadAccountStatement();
            
            Assert.That(statement.Count(), Is.EqualTo(6));
            Assert.That(statement.Select(x => x.Amount).First(), Is.EqualTo(-10));
            Assert.That(statement.Select(x => x.Balance).Last(), Is.EqualTo(10));
        }

        [Test]
        public void Create_account_statement()
        {
            var stetamentResult = @"Date | Amount | Balance \r\n08/07/2016 | -10 | 45 \r\n08/07/2016 | 4 | 55 \r\n08/07/2016 | 40 | 51 \r\n08/07/2016 | -4 | 11 \r\n08/07/2016 | 5 | 15 \r\n08/07/2016 | 10 | 10 \r\n";
            stetamentResult = stetamentResult.Replace(@"\r\n", Environment.NewLine);
            var stetatement = _statementReader.CreateStatement();

            Assert.That(stetatement, Is.EqualTo(stetamentResult));
        }

        private void SetTransactions()
        {
            _transactionBuilder = new StringBuilder();

            _transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(1)} | {10} | {10} ");
            _transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(2)} | {5} | {15} ");
            _transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(3)} | {-4} | {11} ");
            _transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(4)} | {40} | {51} ");
            _transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(5)} | {4} | {55} ");
            _transactionBuilder.AppendLine($"{DateTime.Now.AddSeconds(6)} | {-10} | {45} ");

            using (var sw = _fileSystem.File.AppendText(SafeDepositLocationTest))
            {
                sw.WriteLine(_transactionBuilder);
            }
        }
    }
}
