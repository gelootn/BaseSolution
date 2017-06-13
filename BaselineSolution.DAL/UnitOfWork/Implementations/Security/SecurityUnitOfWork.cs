using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Domain.Shared;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;

namespace BaselineSolution.DAL.UnitOfWork.Implementations.Security
{
    public class SecurityUnitOfWork : UnitOfWork, ISecurityUnitOfWork
    {
        public SecurityUnitOfWork(DatabaseContext context, IGenericRepository<Right> rightRepo, IGenericRepository<User> userRepo, IGenericRepository<SystemLanguage> systemLanguageRepo, IGenericRepository<Account> accountRepo, IGenericRepository<Role> roleRepo) : base(context)
        {

            RightRepo = rightRepo;
            UserRepo = userRepo;
            SystemLanguageRepo = systemLanguageRepo;
            AccountRepo = accountRepo;
            RoleRepo = roleRepo;

        }

        public IGenericRepository<User> UserRepo { get; }

        public IGenericRepository<Right> RightRepo { get; }

        public IGenericRepository<SystemLanguage> SystemLanguageRepo { get; }

        public IGenericRepository<Account> AccountRepo { get; }

        public IGenericRepository<Role> RoleRepo { get; }

    }
}
