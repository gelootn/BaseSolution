using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Security
{
    public interface ISecurityMgntService
    {
        ICrudService<UserBo, UserCommitBo> UserCrudService { get; }


        Response<UserBo> GetUserList();
        Response<bool> SetUserPassword();
    }
}