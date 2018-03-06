using System.Data.Entity;
using System.Linq;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Translators.Security;
using NLog.LayoutRenderers.Wrappers;

namespace BaselineSolution.Service.Security
{
    public class SecurityMgntService : ISecurityMgntService
    {
        private readonly ISecurityUnitOfWork _unitOfWork;
        private readonly IGenericService<UserBo> _userService;
        private readonly IGenericService<RoleBo> _roleService;
        private readonly IGenericService<AccountBo> _accountService;
        private readonly IGenericService<RightBo> _rightService;

        public SecurityMgntService(ISecurityUnitOfWork unitOfWork, IGenericService<UserBo> userService, IGenericService<RoleBo> roleService, IGenericService<AccountBo> accountService, IGenericService<RightBo> rightService)
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
            var result = _unitOfWork.RightRepo.List().Where(x => x.ParentId == null).Include(x => x.Children).ToList();
            return new Response<RightBo>(result.Select(x=> x.ToBo(new RightBoTranslator())).ToList());
        }

        Response<RoleBo> ISecurityMgntService.GetAllowedRoles(int userId)
        {
            var user = _unitOfWork.UserRepo.FindById(userId);
            var roles = user.Roles
                .SelectMany(r => r.Flatten())
                .Distinct()
                .ToList();

            return new Response<RoleBo>(roles.Select(x=> x.ToBo(new RoleBoTranslator())).ToList());
        }

        Response<RestrictedRightBo> ISecurityMgntService.GetRestrictedRights(int userId)
        {
            var result = _unitOfWork.RightRepo.List().Where(x => x.ParentId == null).Include(x => x.Children).ToList();
            var user = _unitOfWork.UserRepo.FindById(userId);
            var translator = new RestrictedRightBoTranslator();
            var response  = result.Select(x => translator.FromModel(x, user)).ToList();
            return new Response<RestrictedRightBo>(response);
        }

        Response<RoleFullBo> ISecurityMgntService.GetFullRole(int roleId)
        {
            var role = _unitOfWork.RoleRepo.FindById(roleId);
            if(role == null)
                return new Response<RoleFullBo>().AddItemNotFound(roleId);

            return new Response<RoleFullBo>(role.ToBo(new RoleFullBoTranslator()));

        }

        Response<bool> ISecurityMgntService.SaveFullRole(RoleFullBo bo, int userId)
        {
            if(!bo.IsValid())
                return new Response<bool>(false).AddValidationMessage(bo.ValidationMessages);
            var role = _unitOfWork.RoleRepo.FindById(bo.Id);

            if(role == null)
                return new Response<bool>().AddItemNotFound(bo.Id);

            role = bo.UpdateModel(role, new RoleFullBoTranslator());
            _unitOfWork.RoleRepo.AddOrUpdate(role);
            _unitOfWork.Commit(userId);

            return new Response<bool>(true);

        }
    }



}
