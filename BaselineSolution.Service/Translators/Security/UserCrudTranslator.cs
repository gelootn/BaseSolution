using System.Linq;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    public class UserCrudTranslator : ICrudTranslator<UserBo, UserCommitBo, User>
    {
        public UserBo ToBo(User model)
        {
            var bo = new UserBo
            {
                Account = new DisplayObject(model.AccountId, model.Account.Name),
                AccountId = model.AccountId,
                Email = model.Email,
                Id = model.Id,
                Name = model.Name,
                FirstName = model.FirstName,
                LastLogin = model.LastLogin,
                LoginCount = model.LoginCount,
                Password = model.Password,
                Username = model.Username,
                Roles = model.Roles.Select(x=> new DisplayObject(x.Id, x.Name)).ToList() 
            };

            return bo;
        }

        public User UpdateModel(UserCommitBo bo, User model)
        {
            model.Email = bo.Email;
            model.FirstName = bo.FirstName;
            model.Name = bo.Name;
            model.AccountId = bo.AccountId;
            model.Username = bo.Username;

            model.Roles.Clear();
            model.Roles = bo.Roles.Select(x => new Role {Id = x}).ToList();

            return model;
        }
    }
}
