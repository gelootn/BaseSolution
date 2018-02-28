using System.Collections.Generic;
using System.Linq;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Seeders.Internal;
using BaselineSolution.Framework.Security;

namespace BaselineSolution.DAL.Seeders.Security
{
    public class UserSeeder : Seeder<User>
    {
        public UserSeeder(DatabaseContext context)
            : base(context)
        {

        }

        public override void Seed()
        {
            SaveChanges();

            var adminAccount = Get<Account>().Single(s => s.Name.Equals(AccountSeeder.MainAccount));
            var adminRole = Get<Role>().Single(r => r.Name.Equals(RoleSeeder.Administrator));

            

            var administrator = new User
            {
                Name = "administrator",
                AccountId = adminAccount.Id,
                Username = "admin",
                Password = PasswordHasher.CreateHash("admin123!"),
                Email = "info@adconsulting.be",
                Roles = new List<Role> { adminRole }
            };

            AddIfNotExists(u => u.Username, administrator);

            SaveChanges();
        }
    }
}
