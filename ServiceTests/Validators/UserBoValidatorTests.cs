using BaselineSolution.Bo.Models.Security;
using NUnit.Framework;

namespace BaselineSolution.Tests.Validators
{
    [TestFixture]
    public class UserBoValidatorTests
    {
        [Test]
        public void NewBoUsernameEmptyNotValidTest()
        {
            var bo = new UserBo();

            bo.Password = "123456";
            bo.PasswordConfirm = "123456";

            Assert.IsFalse(bo.IsValid());
        }

        [Test]
        public void ExistingBoUsernameEmptyNotValidTest()
        {
            var bo = new UserBo();
            bo.Id = 1;
            bo.Password = "123456";
            bo.PasswordConfirm = "123456";

            Assert.IsFalse(bo.IsValid());
        }


        [Test]
        public void ExistingBoNoPasswordIsValidTest()
        {
            var bo = new UserBo();
            bo.Id = 1;
            bo.Username = "user";
            bo.Password = "";
            bo.PasswordConfirm = "";

            Assert.IsTrue(bo.IsValid());
        }

        [Test]
        public void NewBoNoPasswordIsInValidTest()
        {
            var bo = new UserBo();

            bo.Username = "user";
            bo.Password = "";
            bo.PasswordConfirm = "";

            Assert.IsFalse(bo.IsValid());
        }

        [Test]
        public void NewBoNoPasswordMisMatchIsInValidTest()
        {
            var bo = new UserBo();

            bo.Username = "user";
            bo.Password = "1234";
            bo.PasswordConfirm = "3215";

            Assert.IsFalse(bo.IsValid());
        }

    }
}
