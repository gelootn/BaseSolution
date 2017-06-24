using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Internal
{
    public interface ISecurityService : IService
    {
        Response<bool> CheckUserRight(UserSecurityBo user, string rightKey);
        Response<UserSecurityBo> FindUserByUsername(string username);
        Response<RightSecurityBo> GetFullRightList();
        Response<bool> Login(string userName, string password, out UserSecurityBo user);
        Response<SystemLanguageBo> GetLanguages();

    }
}
