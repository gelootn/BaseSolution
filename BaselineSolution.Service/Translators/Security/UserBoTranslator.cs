using System.Linq;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class UserBoTranslator : ITranslator<UserBo, User>
    {
        public  UserBo FromModel( User model)
        {
            var bo = new UserBo();
            bo.Id = model.Id;
            bo.Email = model.Email;
            bo.AccountId = model.AccountId;
            bo.Account = new DisplayObject(model.AccountId, model.Account.Name);
            bo.FirstName = model.FirstName;
            bo.Name = model.Name;
            bo.LastLogin = model.LastLogin;
            bo.LoginCount = model.LoginCount;
            bo.Roles = model.Roles.Select(x => new DisplayObject(x.Id, x.Name)).ToList();

            return bo;
        }

        public  User UpdateModel(UserBo bo, User model)
        {
            model.Id = bo.Id;
            model.Email = bo.Email;
            model.AccountId = bo.AccountId;
            model.FirstName = bo.FirstName;
            model.Name = bo.Name;
            model.LastLogin = bo.LastLogin;
            model.LoginCount = bo.LoginCount;

            //model.Roles.UpdateWith(bo.Roles,tr )

            //bo.Roles = model.Roles.Select(x => new DisplayObject(x.Id, x.Name)).ToList();

            return model;
        }
    }
}
