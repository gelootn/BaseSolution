using System;
using System.Linq;
using System.Linq.Expressions;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> List();
        TEntity FindById(int id);
        TEntity FirstOrDefault(Func<TEntity, bool> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
        void AddOrUpdate(TEntity item);
        void Delete(int id);
        void HardDelete(int id);
        void Commit(int userId);

    }
}