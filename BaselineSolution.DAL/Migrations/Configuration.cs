using BaselineSolution.DAL.Seeders;
using System.Data.Entity.Migrations;

namespace BaselineSolution.DAL.Migrations
{
   

    internal sealed class Configuration : DbMigrationsConfiguration<Database.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Database.DatabaseContext context)
        {
            foreach (var seeder in SeedCollection.GetDefaultSeeders(context))
            {
                seeder.Seed();
            }
            SeedCollection.SaveChanges(context);
        }
    }
}
