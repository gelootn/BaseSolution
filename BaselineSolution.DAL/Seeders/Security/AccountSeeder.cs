using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Seeders.Internal;

namespace BaselineSolution.DAL.Seeders.Security
{
    public class AccountSeeder : Seeder<Account>
    {

        internal const string MainAccount = "Main Account";

        public AccountSeeder(DatabaseContext context) : base(context)
        {
        }

        public override void Seed()
        {
            var adminAccount = new Account
            {
                Name = MainAccount,
                Description = "The Master account"
            };

            AddIfNotExists(a => a.Name, adminAccount);
            SaveChanges();

        }
    }
}
