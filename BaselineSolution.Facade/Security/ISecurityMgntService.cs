using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Framework.Response;
using BaselineSolution.Framework.Services;

namespace BaselineSolution.Facade.Security
{
    public interface ISecurityMgntService
    {
        IListService<UserBo> UserListService { get; } 
        Response<UserBo> GetUserById(int id);
        Response<UserBo> AddOrUpdateUser(UserBo bo);
        Response<bool> DeleteUser(int id); 
        Response<bool> SetUserPassword(UserSetPasswordBo bo);


        IListService<AccountBo> AccountListService { get; } 
        Response<AccountBo> GetAccountById(int id);
        Response<AccountBo> AddOrUpdateAccount(AccountBo bo);
        Response<bool> DeleteAccount(int id);

    }
}