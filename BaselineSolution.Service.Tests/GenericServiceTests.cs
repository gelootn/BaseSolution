using AutoMapper;
using AutoMapper.Configuration;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.Framework.Infrastructure.Sorting;
using BaselineSolution.Framework.Logging;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Infrastructure.Internal;
using BaselineSolution.Service.Tests.Internal;
using BaselineSolution.Service.Tests.TestData;
using BaselineSolution.Service.Translators.Internal;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;


namespace BaselineSolution.Service.Tests
{
    [TestFixture]
    public class GenericServiceTests
    {
        private IGenericService<TestObjectBo> _service;
        private Mock<IGenericRepository<TestObject>> _repository;
        private ITranslator<TestObjectBo, TestObject> _translator;
        private Mock<ILogging> _logger;

        [OneTimeSetUp]
        public void Init()
        {
            Mapper.Initialize(new MapperConfigurationExpression());
        }

        [SetUp]
        public void SetUp()
        {



            _logger = new Mock<ILogging>();

            _repository = new Mock<IGenericRepository<TestObject>>();
            _translator = new TestObjectTranslator();

            _service = new GenericService<TestObjectBo, TestObject>(_repository.Object, _translator, _logger.Object);
        }

        [Test]
        public void GetByIdTest()
        {
            var dbObject = new TestObject { Id = 1, Name = "db object" };

            _repository.Setup(x => x.FindByIdAsync(1))
                .Returns(Task.FromResult(dbObject));

            var response = _service.GetById(1);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.Value.Id, Is.EqualTo(1));
            Assert.That(response.Value.Name, Is.EqualTo(dbObject.Name));
        }

        [Test]
        public void AddNewTest()
        {
            _repository.Setup(x => x.AddOrUpdate(It.IsAny<TestObject>())).Callback<TestObject>(o =>
                o.Id = 25);

            var response = _service.AddOrUpdate(new TestObjectBo {Name = "Add object"}, 0);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.Value, Is.EqualTo(25));

            Assert.That(response.Messages.Count, Is.EqualTo(0));

            _repository.Verify(x => x.Commit(0), Times.Once);
        }

        [Test]
        public void UpdateExistingTest()
        {
            _repository.Setup(x => x.AddOrUpdate(It.IsAny<TestObject>())).Callback<TestObject>(o =>
                o.Id = 25);

            _repository.Setup(x => x.FindById(It.IsAny<int>())).Returns(new TestObject {Id = 25, Name = "Db Name"});

            var response = _service.AddOrUpdate(new TestObjectBo {Id = 25, Name = "Add object"}, 0);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.Value, Is.EqualTo(25));
            Assert.That(response.Messages.Count, Is.EqualTo(0));

            _repository.Verify(x => x.Commit(0), Times.Once);
        }

        [Test]
        public void UpdateNonExistingTest()
        {
            _repository.Setup(x => x.FindById(It.IsAny<int>())).Returns<TestObject>(null);

            var response = _service.AddOrUpdate(new TestObjectBo {Id = 25, Name = "Add object"}, 0);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.HasValue, Is.False);
            Assert.That(response.Messages.Count, Is.EqualTo(1));

            var message = response.Messages.First();

            Assert.That(message.MessageText, Is.EqualTo("Item with Id 25 was not found"));
        }


        [Test]
        public void AddOrUpdateInValidTest()
        {
            _repository.Setup(x => x.FindById(It.IsAny<int>())).Returns<TestObject>(null);

            var response = _service.AddOrUpdate(new TestObjectBo {Id = 150, Name = "Add object"}, 0);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.HasValue, Is.False);
            Assert.That(response.Messages.Count, Is.EqualTo(1));

            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Validation));
        }


        [Test]
        public void AddOrUpdateExceptionTest()
        {
            var errorMessage = "Generic Error Message";
            

            _repository.Setup(x => x.FindById(It.IsAny<int>())).Throws(new Exception(errorMessage));

            var response = _service.AddOrUpdate(new TestObjectBo {Id = 25, Name = "Add object"}, 0);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.HasValue, Is.False);
            Assert.That(response.Messages.Count, Is.EqualTo(1));

            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Error));
            Assert.That(message.MessageText, Is.EqualTo(errorMessage));
        }

        [Test]
        public async Task GetByIdAsyncTest()
        {
            var dbObject = new TestObject { Id = 1, Name = "db object" };

            _repository.Setup(x => x.FindByIdAsync(1))
                .Returns(Task.FromResult(dbObject));

            var response = await _service.GetByIdAsync(1);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);
            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.Value.Id, Is.EqualTo(1));
            Assert.That(response.Value.Name, Is.EqualTo(dbObject.Name));
        }

        [Test]
        public void GetByIdNotFoundTest()
        {
            _repository.Setup(x => x.FindByIdAsync(1))
                .Returns(Task.FromResult<TestObject>(null));

            var response = _service.GetById(1);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.HasValue, Is.False);
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.Messages.Count, Is.EqualTo(1));
            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Error));
            Assert.That(message.MessageText, Is.EqualTo("Item with Id 1 was not found"));

        }

        [Test]
        public void GetByIdThrowsExceptionTest()
        {
            _repository.Setup(x => x.FindByIdAsync(1))
                .Throws(new ArgumentException("Exception"));


            _logger.Setup(x => x.Error(It.IsAny<Exception>()));

            var response = _service.GetById(1);
            Assert.That(response, Is.Not.Null);
            Assert.That(response.HasValue, Is.False);
            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.Messages.Count, Is.EqualTo(1));
            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Error));
            Assert.That(message.MessageText, Is.EqualTo("Exception"));

            _logger.Verify(x => x.Error(It.IsAny<Exception>()), Times.Once);


        }

        [Test]
        public void GetFullListTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = _service.List(EntityFilter<TestObjectBo>.AsQueryable());

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Values.Count, Is.EqualTo(10));
        }

        [Test]
        public void GetBasicFilteredListTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = _service.List(EntityFilter<TestObjectBo>.Where(x => x.Id > 5));

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Values.Count, Is.EqualTo(5));
        }

        [Test]
        public void GetBasicFilteredListThrowsErrorTest()
        {
            const string errorMessage = "Error message";

            _repository.Setup(x => x.List())
                .Throws(new ArgumentException(errorMessage));

            var response = _service.List(EntityFilter<TestObjectBo>.Where(x => x.Id > 5));

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.HasValue, Is.False);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Messages.Count, Is.EqualTo(1));

            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Error));
            Assert.That(message.MessageText, Is.EqualTo(errorMessage));

        }

        [Test]
        public async Task GetAsyncBasicFilteredListTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = await _service.ListAsync(EntityFilter<TestObjectBo>.Where(x => x.Id > 5));

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Values.Count, Is.EqualTo(5));
        }



        [Test]
        public void GetBasicFilteredAndSortedListTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = _service.List(
                EntityFilter<TestObjectBo>.Where(x => x.Id > 5),
                EntitySorter<TestObjectBo>.OrderByDescending(x => x.Id), 0, 100);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Values.Count, Is.EqualTo(5));
            Assert.That(response.Values.First().Id, Is.EqualTo(10));
        }

        [Test]
        public async Task GetAsyncBasicFilteredAndSortedListTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = await _service.ListAsync(
                EntityFilter<TestObjectBo>.Where(x => x.Id > 5),
                EntitySorter<TestObjectBo>.OrderByDescending(x => x.Id), 0, 100);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Values.Count, Is.EqualTo(5));
            Assert.That(response.Values.First().Id, Is.EqualTo(10));
        }


        [Test]
        public void GetBasicFilteredAndSortedAndPagedListTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = _service.List(
                EntityFilter<TestObjectBo>.Where(x => x.Id > 5),
                EntitySorter<TestObjectBo>.OrderByDescending(x => x.Id), 0, 2);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Values.Count, Is.EqualTo(2));
            Assert.That(response.Values.First().Id, Is.EqualTo(10));
            Assert.That(response.Values.Last().Id, Is.EqualTo(9));
        }

        [Test]
        public void DeleteExistingTest()
        {
            _repository.Setup(x => x.FindById(It.IsAny<int>()))
                .Returns(new TestObject {Id = 20});

            _repository.Setup(x => x.Delete(It.IsAny<int>()));

            var response = _service.Delete(10, 0);

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);

            _repository.Verify(x => x.Commit(0), Times.Once);
        }

        [Test]
        public void DeleteNonExistingTest()
        {
            _repository.Setup(x => x.FindById(It.IsAny<int>()))
                .Returns<TestObject>(null);

            var response = _service.Delete(10, 0);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.HasValue, Is.False);

            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Error));
            Assert.That(message.MessageText, Is.EqualTo("Item with Id 10 was not found"));

            _repository.Verify(x => x.Commit(0), Times.Never);

        }

        [Test]
        public void DeleteExceptionTest()
        {
            var errorMessage = "Generic Error Message";
            _repository.Setup(x => x.FindById(It.IsAny<int>()))
                .Throws(new ArgumentException(errorMessage));

            var response = _service.Delete(10, 0);

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.HasValue, Is.False);

            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Error));
            Assert.That(message.MessageText, Is.EqualTo(errorMessage));

            _repository.Verify(x => x.Commit(0), Times.Never);

        }


        [Test]
        public void CountUnfilteredTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = _service.Count(
                EntityFilter<TestObjectBo>.AsQueryable());

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Value, Is.EqualTo(10));

        }

        [Test]
        public void CountFilteredTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = _service.Count(
                EntityFilter<TestObjectBo>.Where(x => x.Id > 5));

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Value, Is.EqualTo(5));

        }

        [Test]
        public async Task CountAsyncFilteredTest()
        {
            var data = GenerateData(10);

            _repository.Setup(x => x.List())
                .Returns(data);

            var response = await _service.CountAsync(
                EntityFilter<TestObjectBo>.Where(x => x.Id > 5));

            Assert.That(response.IsSuccess, Is.True);
            Assert.That(response.HasValue, Is.True);
            Assert.That(response.HasWarnings, Is.False);

            Assert.That(response.Value, Is.EqualTo(5));

        }

        [Test]
        public void CountExceptionTest()
        {
            var errorMessage = "Generic Error Message";
            _repository.Setup(x => x.List())
                .Throws(new ArgumentException(errorMessage));

            var response = _service.Count(EntityFilter<TestObjectBo>.AsQueryable());

            Assert.That(response.IsSuccess, Is.False);
            Assert.That(response.HasValue, Is.False);

            var message = response.Messages.First();

            Assert.That(message.Type, Is.EqualTo(MessageType.Error));
            Assert.That(message.MessageText, Is.EqualTo(errorMessage));

        }

        private DbSet<TestObject> GenerateData(int nrOfItems)
        {
            var list = new List<TestObject>();

            for (int i = 1; i < nrOfItems + 1; i++)
            {
                list.Add(new TestObject { Id = i, Name = $"Test Object {i}" });
            }


            var data = list.AsQueryable();

            var mockSet = new Mock<DbSet<TestObject>>();
            mockSet.As<IDbAsyncEnumerable<TestObject>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<TestObject>(data.GetEnumerator()));

            mockSet.As<IQueryable<TestObject>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<TestObject>(data.Provider));

            mockSet.As<IQueryable<TestObject>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<TestObject>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<TestObject>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return mockSet.Object;

        }
    }
}
