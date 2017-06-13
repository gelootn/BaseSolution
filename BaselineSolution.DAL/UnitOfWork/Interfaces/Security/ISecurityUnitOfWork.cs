using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Domain.Shared;
using BaselineSolution.DAL.Repositories;

namespace BaselineSolution.DAL.UnitOfWork.Interfaces.Security
{
    public interface ISecurityUnitOfWork : IUnitOfWork
    {
        IGenericRepository<User> UserRepo { get; }
        IGenericRepository<Right> RightRepo { get; }
        IGenericRepository<SystemLanguage> SystemLanguageRepo { get; }
        IGenericRepository<Account> AccountRepo { get; }
        IGenericRepository<Role> RoleRepo { get; }


    }
}
