using System.Linq;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Framework.Security;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class UserBoTranslator : ITranslator<UserBo, User>
    {
        public  UserBo FromModel( User model)
        {
            var bo = new UserBo();
            bo.Id = model.Id;
            bo.Username = model.Username;
            bo.Email = model.Email;
            bo.AccountId = model.AccountId;
            bo.Account = new DisplayObject(model.AccountId, model.Account.Name);
            bo.FirstName = model.FirstName;
            bo.Name = model.Name;
            bo.LastLogin = model.LastLogin;
            bo.LoginCount = model.LoginCount;
            bo.Roles = model.Roles.Select(x => x.ToBo(new RoleBoTranslator())).ToList();
            bo.RoleIds = model.Roles.Select(x => x.Id).ToArray();

            return bo;
        }

        public  User UpdateModel(UserBo bo, User model)
        {

            if (bo.IsNew)
                model.Password = PasswordHasher.CreateHash(bo.Password);

            model.Id = bo.Id;
            model.Email = bo.Email;
            model.AccountId = bo.AccountId;
            model.FirstName = bo.FirstName;
            model.Name = bo.Name;
            model.Username = bo.Username;

            return model;
        }
    }
}
