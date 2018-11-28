using System.Collections.Generic;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Logging;
using BaselineSolution.Framework.Response;
using BaselineSolution.WebApi.Controllers;
using BaselineSolution.WebApi.Filters.Account;
using BaselineSolution.WebApi.Infrastructure.Filters;
using NUnit.Framework;
using Rhino.Mocks;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using BaselineSolution.Framework.Infrastructure.Filtering;

namespace BasaelineSolution.WebApi.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private AccountController _controller;
        private IGenericService<AccountBo> _accountService;
        private IFilterHandler<AccountBo, AccountBoFilter> _listFilterHandler;
        private ILogging _logger;

        [SetUp]
        public void SetUp()
        {
            _accountService = MockRepository.GenerateMock<IGenericService<AccountBo>>();
            _listFilterHandler = MockRepository.GenerateMock<IFilterHandler<AccountBo, AccountBoFilter>>();
            _logger = MockRepository.GenerateMock<ILogging>();

            _controller = new AccountController(_accountService, _listFilterHandler, _logger);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();
        }

        [Test]
        public void GetByIdReturnOneAccount()
        {
            var accountBo = new AccountBo { Id = 1, Name = "Test Account" };
            var response = new Response<AccountBo>(accountBo);

            _accountService.Expect(x => x.GetByIdAsync(Arg<int>.Is.Anything))
                .Return(Task.FromResult(response));

            var actionResult = _controller.GetById(1);
            var contentResult = actionResult.Result as OkNegotiatedContentResult<ApiResponse<AccountBo>>;


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

            _accountService.Expect(x => x.ListAsync(null))
                .Return(Task.FromResult(response));

            var actionResult = _controller.List(null);
            var contentResult = actionResult.Result as OkNegotiatedContentResult<ApiResponse<AccountBo>>;


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

            _listFilterHandler.Expect(x => x.CreateFilter(filter))
                .Return(repoFilter);

            _accountService.Expect(x => x.ListAsync(repoFilter))
                .Return(Task.FromResult(serviceResponse));

            var actionResult = _controller.List(filter);
            var contentResult = actionResult.Result as OkNegotiatedContentResult<ApiResponse<AccountBo>>;

            _listFilterHandler.VerifyAllExpectations();
            _accountService.VerifyAllExpectations();

            Assert.That(contentResult, Is.Not.Null);
            Assert.That(contentResult.Content, Is.Not.Null);
            var apiResult = contentResult.Content;

            Assert.That(apiResult.Values.Count, Is.EqualTo(1));
            Assert.That(apiResult.Messages.Count, Is.EqualTo(0));
        }
    }
}
