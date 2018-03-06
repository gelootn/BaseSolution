using System.Data.Entity;
using System.Linq;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Translators.Security;

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

    }



}
