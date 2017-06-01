using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.DAL.Database.Configuration.Internal
{
    /// <summary>
    ///     Provides a default, overridable configuration for entities that inherit from Entity
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity
    /// </typeparam>
    public abstract class EntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity>, IEntityConfiguration
        where TEntity : Entity
    {
        protected EntityConfiguration()
        {
            ConfigureDefaults();
            Configure();
        }

        /// <summary>
        ///     Sets defaults configuration for <typeparamref name="TEntity"/>
        /// </summary>
        protected virtual void ConfigureDefaults()
        {
            /*
             * Configure this table to map inherited properties to a separate table. 
             * This way, abstract classes (such as Entity, Document, ...) do not get a separate table
             */
            Map(u => u.MapInheritedProperties());


            /**
             * Configure this table so that the Id property is an auto-incremented identity column
             */
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        protected abstract void Configure();

        public void Register(ConfigurationRegistrar configurations)
        {
            configurations.Add(this);
        }
    }
}
