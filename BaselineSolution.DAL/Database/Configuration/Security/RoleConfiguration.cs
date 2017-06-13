using BaselineSolution.DAL.Database.Configuration.Internal;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.DAL.Database.Configuration.Security
{
    public class RoleConfiguration : EntityConfiguration<Role>
    {
        protected override void Configure()
        {
            HasOptional(role => role.Parent) // roles have an optional parent
                .WithMany(role => role.Children) // roles can share a common parent
                .HasForeignKey(r => r.ParentId); // the role parent is known through the parent id
        }
    }
}
