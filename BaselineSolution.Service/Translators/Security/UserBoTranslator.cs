using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.Service.Translators.Security
{
    internal static class UserBoTranslator
    {
        public static UserBo ToUserBo(this User user)
        {
            var bo = new UserBo();
            bo.Id = user.Id;
            bo.Email = user.Email;
            bo.AccountId = user.AccountId;
            bo.Account = new DisplayObject(user.AccountId, user.Account.Name);
            bo.FirstName = user.FirstName;
            bo.Name = user.Name;
            bo.LastLogin = user.LastLogin;
            bo.LoginCount = user.LoginCount;
            bo.Roles = user.Roles.Select(x => new DisplayObject(x.Id, x.Name)).ToList();

            return bo;


        }
    }
}
