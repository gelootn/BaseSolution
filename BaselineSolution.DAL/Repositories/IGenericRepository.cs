using System.Linq;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetAll();
        TEntity Find(int id);
        TEntity AddOrUpdate(TEntity iten);
        void Delete(int id);


    }
}