using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Internal;

namespace BaselineSolution.Facade.Security
{
    public interface ISecurityMgntService
    {
        IGenericService<UserBo> UserService { get; } 
        IGenericService<RoleBo> RoleService { get; }
        IGenericService<AccountBo> AccountService { get; }


    }
}