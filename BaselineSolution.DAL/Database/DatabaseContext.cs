using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using BaselineSolution.DAL.Database.Configuration.Internal;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Database
{
    public class DatabaseContext : DbContext
    {
        private IDictionary<Type, IEntityConfiguration> _configurationDictionary;

        public DatabaseContext()
            : this("Name=Context")
        {

        }

        public DatabaseContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        protected DatabaseContext(DbCompiledModel model)
            : base(model)
        {
        }

        public DatabaseContext(string nameOrConnectionString, DbCompiledModel model)
            : base(nameOrConnectionString, model)
        {
        }

        public DatabaseContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {
        }

        public DatabaseContext(DbConnection existingConnection, DbCompiledModel model, bool contextOwnsConnection)
            : base(existingConnection, model, contextOwnsConnection)
        {
        }

        public DatabaseContext(ObjectContext objectContext, bool dbContextOwnsObjectContext)
            : base(objectContext, dbContextOwnsObjectContext)
        {
        }

        private IDictionary<Type, IEntityConfiguration> ConfigurationDictionary
        {
            get { return _configurationDictionary ?? (_configurationDictionary = new Dictionary<Type, IEntityConfiguration>()); }
        }

        /// <summary>
        ///     Registers the <typeparamref name="TEntity" /> type with the provided <paramref name="configuration" />
        ///     If a configuration for this <typeparamref name="TEntity" /> already exists, it is overwritten.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to configure</typeparam>
        protected void Register<TEntity>(EntityConfiguration<TEntity> configuration)
            where TEntity : Entity
        {
            ConfigurationDictionary[typeof(TEntity)] = configuration;
        }

        protected virtual void RegisterConfigurations()
        {
            // Register new configurations here
            // Register(new [configuration])
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            RegisterConfigurations();
            foreach (var configuration in ConfigurationDictionary.Values)
            {
                configuration.Register(modelBuilder.Configurations);
            }
            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        // ADD DbSets HERE

        #endregion
    }
}
