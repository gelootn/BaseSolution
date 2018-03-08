using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Framework.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class UserSetPasswordBoTranslator : ITranslator<UserSetPasswordBo, User>
    {
        public UserSetPasswordBo FromModel(User model)
        {
            var bo = new UserSetPasswordBo();
            bo.Id = model.Id;

            return bo;
        }

        public User UpdateModel(UserSetPasswordBo bo, User model)
        {
            model.Password = PasswordHasher.CreateHash(bo.Password);
            return model;
        }
    }
}
