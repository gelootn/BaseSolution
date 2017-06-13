using BaselineSolution.DAL.Database.Configuration.Internal;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.DAL.Database.Configuration.Security
{
    public class UserConfiguration : EntityConfiguration<User>
    {
        protected override void Configure()
        {
            HasMany(u => u.Roles).WithMany();
        }
    }
}
