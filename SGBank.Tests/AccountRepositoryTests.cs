using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SGBank.Data;

namespace SGBank.Tests
{
    [TestFixture]
    public class AccountRepositoryTests
    {
        [Test]
        public void CanLoadAccount()
        {
            var repo = new AccountRepository();

            var account = repo.LoadAccount(1);

            Assert.AreEqual(1, account.AccountNumber);
            Assert.AreEqual("Mary", account.FirstName);
        }

        [Test]
        public void UpdateAccountSucceeds()
        {
            var repo = new AccountRepository();
            var accountToUpdate = repo.LoadAccount(1);
            accountToUpdate.Balance = 500.00M;
            repo.UpdateAccount(accountToUpdate);

            var result = repo.LoadAccount(1);

            Assert.AreEqual(500.00M, result.Balance);
        }
    }
}
