using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;

namespace BaselineSolution.Service.Security
{
    public class SecurityMgntService : ISecurityMgntService
    {
        private readonly ISecurityUnitOfWork _unitOfWork;
        private readonly IGenericService<UserBo> _userService;
        private readonly IGenericService<RoleBo> _roleService;
        private readonly IGenericService<AccountBo> _accountService;

        public SecurityMgntService(ISecurityUnitOfWork unitOfWork, IGenericService<UserBo> userService, IGenericService<RoleBo> roleService, IGenericService<AccountBo> accountService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _roleService = roleService;
            _accountService = accountService;
            InitMapper();
        }

        IGenericService<UserBo> ISecurityMgntService.UserService => _userService;

        IGenericService<RoleBo> ISecurityMgntService.RoleService => _roleService;

        IGenericService<AccountBo> ISecurityMgntService.AccountService => _accountService;


        public void SomeMethod()
        {
           
        }

        private void InitMapper()
        {
            /*Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<AccountBo, Account>();
                cfg.CreateMap<UserBo, User>();
                cfg.CreateMap<RoleBo, Role>();
            });*/
        }
    }



}
