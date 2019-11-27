using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Logging;
using BaselineSolution.Framework.Response;
using BaselineSolution.WebApi.Controllers;
using BaselineSolution.WebApi.Filters.Account;
using BaselineSolution.WebApi.Infrastructure.Filters;
using NUnit.Framework;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.WebApi.ViewModels.Account;
using Moq;

namespace BasaelineSolution.WebApi.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private Mock<IGenericService<AccountBo>> _accountService;
        private Mock<IFilterHandler<AccountBo, AccountBoFilter>> _listFilterHandler;
        private Mock<ILogging> _logger;

        [SetUp]
        public void SetUp()
        {


            _accountService = new Mock<IGenericService<AccountBo>>(); 
            _listFilterHandler = new Mock<IFilterHandler<AccountBo, AccountBoFilter>>();
            _logger = new Mock<ILogging>();

            _controller = new AccountController(_accountService.Object, _listFilterHandler.Object, _logger.Object);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();
        }

        [Test]
        public void GetByIdReturnOneAccount()
        {
            var accountBo = new AccountBo { Id = 1, Name = "Test Account" };
            var response = new Response<AccountBo>(accountBo);

            _accountService.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(response);

            var actionResult = _controller.GetById(1);
            var contentResult = actionResult as OkNegotiatedContentResult<ApiResponse<AccountViewModel>>;


            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);
            var apiResult = contentResult.Content;

            Assert.That(apiResult.Values.Count, Is.EqualTo(1));
            Assert.That(apiResult.Messages.Count, Is.EqualTo(0));
        }

        [Test]
        public void ListWithEmptyFilter()
        {
            var accountOne = new AccountBo { Id = 1, Name = "Test Account" };
            var accountTwo = new AccountBo { Id = 2, Name = "Second Account" };

            var response = new Response<AccountBo>(new List<AccountBo>{accountOne, accountTwo});

 
            _accountService.Setup(x => x.List(null))
                .Returns(response);

            var actionResult = _controller.List(null);
            var contentResult = actionResult as OkNegotiatedContentResult<ApiResponse<AccountViewModel>>;


            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);
            var apiResult = contentResult.Content;

            Assert.That(apiResult.Values.Count, Is.EqualTo(2));
            Assert.That(apiResult.Messages.Count, Is.EqualTo(0));
        }

        [Test]
        public void ListWithFilter()
        {
            var accountOne = new AccountBo { Id = 1, Name = "Test Account" };

            var serviceResponse = new Response<AccountBo>(new List<AccountBo>{accountOne});
            var filter = new AccountBoFilter{ Name = "Test" };
            var repoFilter = EntityFilter<AccountBo>.Where(x => x.Name.Contains(filter.Name));

            _listFilterHandler.Setup(x => x.CreateFilter(filter))
                .Returns(repoFilter);

            _accountService.Setup(x => x.List(repoFilter))
                .Returns(serviceResponse);

            var actionResult = _controller.List(filter);
            var contentResult = actionResult as OkNegotiatedContentResult<ApiResponse<AccountViewModel>>;

            _listFilterHandler.VerifyAll();
            _accountService.VerifyAll();

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);
            var apiResult = contentResult.Content;

            Assert.That(apiResult.Values.Count, Is.EqualTo(1));
            Assert.That(apiResult.Messages.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddNewAccount()
        {
            var account = new AccountBo{ Id = 0, Name = "Test Account"};
            var serviceResponse = new Response<int>(10);


            _accountService.Setup(x => x.AddOrUpdate(It.IsAny<AccountBo>(), 0))
                .Returns(serviceResponse);

            var actionResult = _controller.Add(account);
            var contentResult = actionResult as OkNegotiatedContentResult<ApiResponse<int>>;
            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);

            var apiResult = contentResult.Content;
            Assert.That(apiResult.Values.Count, Is.EqualTo(1));
            Assert.That(apiResult.Messages.Count, Is.EqualTo(0));
            Assert.That(apiResult.Values.First(), Is.EqualTo(10));
        }


        [Test]
        public void UpdateAccount()
        {
            var account = new AccountBo{ Id = 0, Name = "Test Account"};
            var serviceResponse = new Response<int>(10);


            _accountService.Setup(x => x.AddOrUpdate(It.IsAny<AccountBo>(), 0))
                .Returns(serviceResponse);

            var actionResult = _controller.Update(account);
            var contentResult = actionResult as OkNegotiatedContentResult<ApiResponse<int>>;
            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);

            var apiResult = contentResult.Content;
            Assert.That(apiResult.Values.Count, Is.EqualTo(1));
            Assert.That(apiResult.Messages.Count, Is.EqualTo(0));
            Assert.That(apiResult.Values.First(), Is.EqualTo(10));
        }


        [Test]
        public void DeleteAccount()
        {
           
            var serviceResponse = new Response<bool>(true);


            _accountService.Setup(x => x.Delete(10, 0))
                .Returns(serviceResponse);

            var actionResult = _controller.Delete(10);
            var contentResult = actionResult as OkNegotiatedContentResult<ApiResponse<bool>>;
            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);

            var apiResult = contentResult.Content;
            Assert.That(apiResult.Values.Count, Is.EqualTo(1));
            Assert.That(apiResult.Messages.Count, Is.EqualTo(0));
            Assert.That(apiResult.Values.First(), Is.EqualTo(true));
        }
    }
}
