using AutoMapper;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Migrations;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;

namespace BaselineSolution.Service.Security
{
    public class SecurityMgntService : ISecurityMgntService
    {
        private readonly IGenericService<UserBo> _userService;
        private readonly IGenericService<RoleBo> _roleService;
        private readonly IGenericService<AccountBo> _accountService;

        public SecurityMgntService(IGenericService<UserBo> userService, IGenericService<RoleBo> roleService, IGenericService<AccountBo> accountService)
        {
            _userService = userService;
            _roleService = roleService;
            _accountService = accountService;
        }

        IGenericService<UserBo> ISecurityMgntService.UserService => _userService;

        IGenericService<RoleBo> ISecurityMgntService.RoleService => _roleService;

        IGenericService<AccountBo> ISecurityMgntService.AccountService => _accountService;


        private void InitMapper()
        {
            
        }
    }



}
