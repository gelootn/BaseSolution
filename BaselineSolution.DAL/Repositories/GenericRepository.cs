using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = query.Where(x => !x.Deleted);
            return query;

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

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).Count();
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

        public void Commit(int userId)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                DateTime now = DateTime.Now;
                foreach (var entry in _context.ChangeTracker.Entries<Entity>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreationDate = now;
                            entry.Entity.CreationUserId = userId;
                            break;
                        case EntityState.Modified:
                            entry.Entity.ModificationDate = now;
                            entry.Entity.ModificationUserId = userId;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.Entity.Deleted = true;
                            break;
                    }
                }
                try
                {
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
