using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.Framework.Infrastructure.Contracts;
using LinqKit;

namespace BaselineSolution.DAL.Seeders.Internal
{
    public abstract class Seeder<TEntity> : ISeed where TEntity : class, IIdentifiable, IDeletable
    {
        private readonly DatabaseContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        protected Seeder(DatabaseContext context)
        {
            _context = context;
        }

        protected IQueryable<TTable> Get<TTable>() where TTable : Entity
        {
            return _context.Set<TTable>().Where(t => !t.Deleted);
        }

        protected void AddIfNotExists(IEnumerable<TEntity> entities, Expression<Func<TEntity, string>>[] strings = null, Expression<Func<TEntity, int>>[] ints = null, Expression<Func<TEntity, decimal>>[] decimals = null)
        {
            if (entities == null)
                return;
            var entitySet = _context.Set<TEntity>();
            foreach (var entity in entities)
            {
                var list = entitySet.AsExpandable();
                //STRING
                if (strings != null)
                {
                    foreach (var property in strings)
                    {
                        var value = property.Compile()(entity);

                        list = list.Where(e => property.Invoke(e).Equals(value));
                    }

                }

                if (ints != null)
                {
                    foreach (var property in ints)
                    {
                        var value = property.Compile()(entity);

                        list = list.Where(e => property.Invoke(e).Equals(value));
                    }
                }

                if (decimals != null)
                {
                    foreach (var property in decimals)
                    {
                        var value = property.Compile()(entity);

                        list = list.Where(e => property.Invoke(e).Equals(value));
                    }
                }



                if (!list.Any(e => !e.Deleted))
                {
                    entitySet.Add(entity);
                }
            }

        }


        protected void AddIfNotExists(Expression<Func<TEntity, string>> property, params TEntity[] entities)
        {
            if (entities == null)
                return;
            var entitySet = _context.Set<TEntity>();
            foreach (var entity in entities)
            {
                var value = property.Compile()(entity);
                if (!entitySet.AsExpandable().Any(e => property.Invoke(e) == value && !e.Deleted))
                {
                    entitySet.Add(entity);
                }
            }

        }

        protected void AddIfNotExists(Expression<Func<TEntity, int>> property, params TEntity[] entities)
        {
            if (entities == null)
                return;
            var entitySet = _context.Set<TEntity>();
            foreach (var entity in entities)
            {
                var value = property.Compile()(entity);
                if (!entitySet.AsExpandable().Any(e => property.Invoke(e) == value && !e.Deleted))
                {
                    entitySet.Add(entity);
                }
            }

        }

        public void SaveChanges()
        {
            SeedCollection.SaveChanges(_context);
        }


        public abstract void Seed();
    }
}
