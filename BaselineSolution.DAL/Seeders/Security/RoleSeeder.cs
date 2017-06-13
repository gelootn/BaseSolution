using System;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Seeders.Internal;

namespace BaselineSolution.DAL.Seeders.Security
{
    public class RoleSeeder : Seeder<Role>
    {
        internal const string Administrator = "Administrator";

        

        public RoleSeeder(DatabaseContext context)
            : base(context)
        {
        }

        public override void Seed()
        {
            var administrator = new Role
            {
                Name = Administrator,
                CreationUserId = 0,
                CreationDate = DateTime.Now
            };

            AddIfNotExists(r => r.Name, administrator);
            SaveChanges();
        }
    }
}
