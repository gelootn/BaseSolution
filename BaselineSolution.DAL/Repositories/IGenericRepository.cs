using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> List(Expression<Func<TEntity, bool>> predicate, int page, int pageSize, out int totalCount);
        IQueryable<TEntity> List();
        TEntity FindById(int id);
        TEntity FirstOrDefault(Func<TEntity, bool> predicate);
        int Count(Expression<Func<TEntity, bool>> predicate);
        void AddOrUpdate(TEntity item);
        void Delete(int id);
        void HardDelete(int id);


    }
}