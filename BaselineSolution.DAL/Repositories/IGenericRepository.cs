using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> List();

        TEntity FindById(int id);
        Task<TEntity> FindByIdAsync(int id);

        TEntity FirstOrDefault(Func<TEntity, bool> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        void AddOrUpdate(TEntity item);
        void Delete(int id);
        void HardDelete(int id);
        void Commit(int userId);

    }
}