using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Domain.Shared;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.DAL.UnitOfWork.Implementations.Security;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.Framework.Infrastructure.Sorting;
using BaselineSolution.Framework.Services;
using BaselineSolution.Service.Security;
using NUnit.Framework;

namespace ServiceTests
{
    [TestFixture]
    public class TestsForIListService
    {
        private IListService<UserBo> _listService;

        [SetUp]
        public void SetUp()
        {
            var context = new DatabaseContext("Data Source=localhost;Initial Catalog=BaselineDb;Integrated Security = true;MultipleActiveResultSets=True;");

            _listService = new UserListService(new SecurityUnitOfWork(context, new GenericRepository<Right>(context), new GenericRepository<User>(context),new GenericRepository<SystemLanguage>(context),new GenericRepository<Account>(context),new GenericRepository<Role>(context)));
        }

        [Test]
        public void UnFilterdCountTest()
        {
            var result = _listService.Count(null);
            
            Assert.That(result > 0);
        }

        [Test]
        public void FilterdCountTest()
        {
            var filter = EntityFilter<UserBo>.Where(x => x.Name == "LALA" && x.Email.Contains("@") && x.Roles.Any(y => y.Display == "test"));
            var result = _listService.Count(filter);
            
            Assert.That(result == 0);
        }


        [Test]
        public void FilterdListTest()
        {
            var filter = EntityFilter<UserBo>.Where(x => x.Name == "LALA");
            var result = _listService.List(EntitySorter<UserBo>.AsQueryable(), filter, 1,50);
            
            Assert.That(!result.Any());
        }

        [Test]
        public void SortedListTest()
        {
            var filter = EntityFilter<UserBo>.AsQueryable();
            var sorter = EntitySorter<UserBo>.OrderBy(x => x.Id).ThenBy(x => x.Name);
            var result = _listService.List(sorter, filter, 1, 50);

            Assert.That(!result.Any());
        }
    }
}
