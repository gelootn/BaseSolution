using System.Linq;
using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Domain.Shared;

namespace BaselineSolution.Service.Translators.Internal
{
    internal static class SecurityTranslator
    {
        internal static UserSecurityBo ToUserSecurityBo(this User model)
        {
            var user = new UserSecurityBo();
            user.Id = model.Id;
            user.UserName = model.Username;
            user.Email = model.Email;
            user.Account = model.Account.ToAccountSecurity();
            if (model.Roles.Any())
            {
                user.Roles = model.Roles.Select(x => x.ToRoleSecurityBo()).ToList();
            }


            return user;
        }

        private static AccountSecurityBo ToAccountSecurity(this Account model)
        {
            if (model == null)
                return null;

            var account = new AccountSecurityBo();
            account.Id = model.Id;
            account.ParentId = model.ParentId;

            if (model.Children.Any())
            {
                account.Children = model.Children.Select(x => x.ToAccountSecurity()).ToList();
            }

            
            return account;

        }

        private static RoleSecurityBo ToRoleSecurityBo(this Role model)
        {
            var role = new RoleSecurityBo();
            role.Id = model.Id;
            role.Name = model.Name;
            role.ParentId = model.ParentId;

            if (model.Children.Any())
            {
                role.Children = model.Children.Select(x => x.ToRoleSecurityBo()).ToList();
            }

            if (model.RoleRights.Any())
            {
                role.RoleRights = model.RoleRights.Select(x => x.ToRoleRightSecurityBo()).ToList();
            }
            return role;
        }

        private static RoleRightSecurityBo ToRoleRightSecurityBo(this RoleRight model)
        {
            if (model == null) return null;

            var roleRight = new RoleRightSecurityBo();
            roleRight.Id = model.Id;
            roleRight.Allow = model.Allow;
            roleRight.RightId = model.RightId;
            roleRight.Right = model.Right.ToRightSecurityBo();

            return roleRight;
        }

        internal static RightSecurityBo ToRightSecurityBo(this Right model)
        {
            if (model == null)
                return null;

            var right = new RightSecurityBo();
            right.Id = model.Id;
            right.Key = model.Key;
            right.Parent = model.Parent.ToRightSecurityBo();

            return right;

        }

        internal static SystemLanguageBo ToSystemLanguageBo(this SystemLanguage model)
        {
            var language = new SystemLanguageBo();
            language.Id = model.Id;
            language.Culture = model.Culture;

            return language;
        }
    }
}
