using System.Data.Entity;
using BaselineSolution.DAL.Database;

namespace BaselineSolution.Service.Tests.TestData
{
    public class TestDbContext : DatabaseContext
    {
        public virtual DbSet<TestObject> TestObjects { get; set; }
    }
}
