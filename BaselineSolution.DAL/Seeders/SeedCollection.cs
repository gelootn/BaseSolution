using System;
using System.Collections.Generic;
using System.Data.Entity;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.DAL.Seeders.Internal;

namespace BaselineSolution.DAL.Seeders
{
    public static class SeedCollection
    {
        public static IEnumerable<ISeed> GetDefaultSeeders(DatabaseContext context)
        {
            return new ISeed[]
                {
                    // Add seeders here


                };
        }

        public static int SaveChanges(DatabaseContext context)
        {
            DateTime now = DateTime.Now;
            const int UserId = 0;

            foreach (var entry in context.ChangeTracker.Entries<Entity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDate = now;
                        entry.Entity.CreationUserId = UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModificationDate = now;
                        entry.Entity.ModificationUserId = UserId;
                        break;
                }
            }

            return context.SaveChanges();
        }
    }
}
