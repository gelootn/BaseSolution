using System;
using System.Data.Entity;
using System.Linq;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        private readonly DatabaseContext _context;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
        }

        public IQueryable<TEntity> List()
        {
            IQueryable<TEntity> querry = _context.Set<TEntity>();
            querry = querry.Where(x => !x.Deleted);
            return querry;

        }

        public TEntity FindById(int id)
        {
            var item = _context.Set<TEntity>().Find(id);
            if (item != null)
            {
                if (!item.Deleted)
                    return item;
            }

            return null;
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(x => !x.Deleted).FirstOrDefault(predicate);
        }

        public void AddOrUpdate(TEntity item)
        {
            if (item.Id == 0)
            {
                _context.Set<TEntity>().Add(item);
            }
            else
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var item = _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Deleted = true;
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public void HardDelete(int id)
        {
            var item = _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _context.Set<TEntity>().Remove(item);
            }
        }


    }
}
