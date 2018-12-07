using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Bo.Models.Security;
using NUnit.Framework;

namespace BaselineSolution.Tests.Validators
{
    [TestFixture]
    public class AccountBoValidatorTests
    {
        [Test]
        public void ValidAccountBo()
        {
            var account = new AccountBo
            {
                Name = "test"
            };

            Assert.That(account.IsValid, Is.True);
        }

        
        [Test]
        public void InvalidValidAccountBo()
        {
            var account = new AccountBo
            {
                Name = ""
            };

            Assert.That(account.IsValid, Is.False);
        }
    }
}
