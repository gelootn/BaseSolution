using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Logging;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Infrastructure.Internal;
using BaselineSolution.Service.Translators.Security;
using System;
using System.Data.Entity;
using System.Linq;

namespace BaselineSolution.Service.Security
{
    public class SecurityMgntService : BaseService, ISecurityMgntService
    {
        private readonly ISecurityUnitOfWork _unitOfWork;
        private readonly IGenericService<UserBo> _userService;
        private readonly IGenericService<RoleBo> _roleService;
        private readonly IGenericService<AccountBo> _accountService;
        private readonly IGenericService<RightBo> _rightService;

        public SecurityMgntService(
            ILogging log,
            ISecurityUnitOfWork unitOfWork,
            IGenericService<UserBo> userService,
            IGenericService<RoleBo> roleService,
            IGenericService<AccountBo> accountService,
            IGenericService<RightBo> rightService) : base(log)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _roleService = roleService;
            _accountService = accountService;
            _rightService = rightService;
        }

        IGenericService<UserBo> ISecurityMgntService.UserService => _userService;

        IGenericService<RoleBo> ISecurityMgntService.RoleService => _roleService;

        IGenericService<AccountBo> ISecurityMgntService.AccountService => _accountService;

        IGenericService<RightBo> ISecurityMgntService.RightService => _rightService;

        Response<RightBo> ISecurityMgntService.GetTopLevelRights()
        {
            try
            {
                var result = _unitOfWork.RightRepo.List().Where(x => x.ParentId == null).Include(x => x.Children).ToList();
                return new Response<RightBo>(result.Select(x => x.ToBo(new RightBoTranslator())).ToList());
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<RightBo>().AddErrorMessage(e);
            }

        }

        Response<RoleBo> ISecurityMgntService.GetAllowedRoles(int userId)
        {
            try
            {
                var user = _unitOfWork.UserRepo.FindById(userId);
                var roles = user.Roles
                    .SelectMany(r => r.Flatten())
                    .Distinct()
                    .ToList();

                return new Response<RoleBo>(roles.Select(x => x.ToBo(new RoleBoTranslator())).ToList());
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<RoleBo>().AddErrorMessage(e);
            }

        }

        Response<RestrictedRightBo> ISecurityMgntService.GetRestrictedRights(int userId)
        {
            try
            {
                var result = _unitOfWork.RightRepo.List().Where(x => x.ParentId == null).Include(x => x.Children).ToList();
                var user = _unitOfWork.UserRepo.FindById(userId);
                var translator = new RestrictedRightBoTranslator();
                var response = result.Select(x => translator.FromModel(x, user)).ToList();
                return new Response<RestrictedRightBo>(response);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<RestrictedRightBo>().AddErrorMessage(e);
            }
        }

        Response<RoleFullBo> ISecurityMgntService.GetFullRole(int roleId)
        {
            try
            {
                var role = _unitOfWork.RoleRepo.FindById(roleId);
                if (role == null)
                    return new Response<RoleFullBo>().AddItemNotFound(roleId);

                return new Response<RoleFullBo>(role.ToBo(new RoleFullBoTranslator()));
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<RoleFullBo>().AddErrorMessage(e);
            }


        }

        Response<bool> ISecurityMgntService.SaveFullRole(RoleFullBo bo, int userId)
        {
            try
            {
                if (!bo.IsValid())
                    return new Response<bool>(false).AddValidationMessage(bo.ValidationMessages);
                var role = _unitOfWork.RoleRepo.FindById(bo.Id);

                if (role == null)
                    return new Response<bool>().AddItemNotFound(bo.Id);

                role = bo.UpdateModel(role, new RoleFullBoTranslator());
                _unitOfWork.RoleRepo.AddOrUpdate(role);
                _unitOfWork.Commit(userId);

                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<bool>().AddErrorMessage(e);
            }

        }

        Response<bool> ISecurityMgntService.IsUsernameTaken(string name, int userId)
        {
            try
            {
                var user = _unitOfWork.UserRepo.FirstOrDefault(x => x.Username.Equals(name) && x.Id != userId);
                if (user == null)
                    return new Response<bool>(true);
                return new Response<bool>(false);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<bool>().AddErrorMessage(e);
            }
        }

        Response<int> ISecurityMgntService.SaveUser(UserBo bo, int userId)
        {
            try
            {
                if (!bo.IsValid())
                    return new Response<int>().AddValidationMessage(bo.ValidationMessages);

                var user = bo.IsNew ? new User() : _unitOfWork.UserRepo.FindById(bo.Id);

                bo.UpdateModel(user, new UserBoTranslator());

                var roles = _unitOfWork.RoleRepo.List().Where(x => bo.RoleIds.Contains(x.Id));
                user.Roles = roles.ToList();

                _unitOfWork.UserRepo.AddOrUpdate(user);
                _unitOfWork.Commit(user.Id);

                return new Response<int>(user.Id);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<int>().AddErrorMessage(e);
            }
        }

        Response<bool> ISecurityMgntService.ResetUserPassword(UserSetPasswordBo password, int userId)
        {

            try
            {
                if (!password.IsValid())
                    return new Response<bool>().AddValidationMessage(password.ValidationMessages);

                var user = _unitOfWork.UserRepo.FindById(password.Id);
                if (user == null)
                    return new Response<bool>().AddItemNotFound(password.Id);

                password.UpdateModel(user, new UserSetPasswordBoTranslator());
                _unitOfWork.UserRepo.AddOrUpdate(user);
                _unitOfWork.Commit(userId);

                return new Response<bool>(true).AddSuccessMessage(UserBoResource.PasswordReset);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<bool>().AddErrorMessage(e);
            }

        }
    }



}
