using System;
using System.Data.Entity;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.DAL.UnitOfWork.Interfaces;

namespace BaselineSolution.DAL.UnitOfWork.Implementations
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        protected UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public virtual void Commit(int userId)
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
