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
        /// <summary>
        /// Get all rights in a parent child tree
        /// </summary>
        /// <returns></returns>
        Response<RightBo> GetTopLevelRights();

        /// <summary>
        /// Get the allowed roles for one users
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Response<RoleBo> GetAllowedRoles(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Response<RestrictedRightBo> GetRestrictedRights(int userId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Response<RoleFullBo> GetFullRole(int roleId);

        /// <summary>
        /// Save a full Role Tree
        /// </summary>
        /// <param name="bo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Response<bool> SaveFullRole(RoleFullBo bo, int userId);

        /// <summary>
        /// Check if the given username already exitst in the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="userId">userId to exclude from search</param>
        /// <returns></returns>
        Response<bool> IsUsernameTaken(string name, int userId);

        /// <summary>
        /// Save the user, Generic service does not handle the Roles
        /// </summary>
        /// <param name="user"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Response<int> SaveUser(UserBo user, int userId);

        /// <summary>
        /// Reset the password for a user
        /// </summary>
        /// <param name="password"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Response<bool> ResetUserPassword(UserSetPasswordBo password, int userId);

    }
}