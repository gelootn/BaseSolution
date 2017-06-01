using System.Data.Entity.ModelConfiguration.Configuration;

namespace BaselineSolution.DAL.Database.Configuration.Internal
{
    public interface IEntityConfiguration
    {
        void Register(ConfigurationRegistrar configurations);
    }
}
