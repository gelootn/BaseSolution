using System;
using System.Linq;
using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.Bo.Resources;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Response;
using BaselineSolution.Framework.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Infrastructure.Internal
{
    public class SecurityService : BaseService, ISecurityService
    {
        private readonly ISecurityUnitOfWork _unitOfWork;

        public SecurityService(ISecurityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        Response<bool> ISecurityService.CheckUserRight(UserSecurityBo user, string rightKey)
        {
            var dbUser = _unitOfWork.UserRepo.FindById(user.Id);
            if (dbUser == null)
                return new Response<bool>().AddItemNotFound(user.Id);

            bool hasRight = dbUser.Roles.SelectMany(x => x.RoleRights).Any(x => x.Right.Key == rightKey);

            return new Response<bool>(hasRight);
        }


        Response<UserSecurityBo> ISecurityService.FindUserByUsername(string username)
        {
            var dbUser = _unitOfWork.UserRepo.FirstOrDefault(x => x.Username == username);
            if (dbUser == null)
                return new Response<UserSecurityBo>();

            return new Response<UserSecurityBo>(dbUser.ToUserSecurityBo());
        }

        Response<RightSecurityBo> ISecurityService.GetFullRightList()
        {
            var rights = _unitOfWork.RightRepo.List();

            var rightsBos = rights.ToList().SelectMany(x => x.Flatten()).Select(x => x.ToRightSecurityBo());

            return new Response<RightSecurityBo>(rightsBos.ToList());
        }

        Response<bool> ISecurityService.Login(string userName, string password, out UserSecurityBo user)
        {
            var dbUser = _unitOfWork.UserRepo.FirstOrDefault(x => x.Username == userName);
            if (dbUser != null)
            {
                bool passwordsMatch = PasswordHasher.ValidatePassword(password, dbUser.Password);
                if (passwordsMatch)
                {
                    user = dbUser.ToUserSecurityBo();
                    dbUser.LastLogin = DateTime.Now;
                    dbUser.LoginCount = dbUser.LoginCount.GetValueOrDefault(0) + 1;
                    _unitOfWork.Commit(dbUser.Id);
                    return new Response<bool>(true);
                }
                user = null;
                return new Response<bool>(false).AddErrorMessage(BoResources.PasswordsDoNotMatch);
            }
            user = null;
            return new Response<bool>(false).AddErrorMessage(BoResources.UserNotFound);
        }

        Response<SystemLanguageBo> ISecurityService.GetLanguages()
        {
            var languages = _unitOfWork.SystemLanguageRepo.List();

            var languageBos = languages.ToList().Select(x => x.ToSystemLanguageBo()).ToList();

            return new Response<SystemLanguageBo>(languageBos);
        }
    }
}
