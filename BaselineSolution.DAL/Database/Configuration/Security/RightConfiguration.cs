using BaselineSolution.DAL.Database.Configuration.Internal;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.DAL.Database.Configuration.Security
{
    public class RightConfiguration : EntityConfiguration<Right>
    {
        protected override void Configure()
        {
            HasOptional(right => right.Parent) // rights have an optional parent
                .WithMany(right => right.Children) // rights can share a common parent
                .HasForeignKey(r => r.ParentId); // the parent is known through the parent id
        }
    }
}
