using BaselineSolution.Bo.Filters.Security;
using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Security
{
    public interface ISecurityMgntService
    {
        Response<UserBo> GetUserList(UserFilter filter);
        Response<bool> SetUserPassword(UserSetPasswordBo bo);


    }
}