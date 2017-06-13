using BaselineSolution.DAL.Database.Configuration.Internal;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.DAL.Database.Configuration.Security
{
    public class AccountConfiguration : EntityConfiguration<Account>
    {
        protected override void Configure()
        {
            HasOptional(d => d.Parent).WithMany(d => d.Children).HasForeignKey(d => d.ParentId);
        }
    }
}
