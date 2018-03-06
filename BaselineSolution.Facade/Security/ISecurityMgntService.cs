using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Security
{
    public interface ISecurityMgntService
    {
        IGenericService<UserBo> UserService { get; }
        IGenericService<RoleBo> RoleService { get; }
        IGenericService<AccountBo> AccountService { get; }
        IGenericService<RightBo> RightService { get; }
        Response<RightBo> GetTopLevelRights();

    }
}