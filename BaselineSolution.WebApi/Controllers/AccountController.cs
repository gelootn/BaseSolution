using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Logging;
using BaselineSolution.Framework.Response;
using BaselineSolution.WebApi.Filters.Account;
using BaselineSolution.WebApi.Infrastructure.Controllers;
using BaselineSolution.WebApi.Infrastructure.Filters;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BaselineSolution.WebApi.ViewModels.Account;

namespace BaselineSolution.WebApi.Controllers
{
    /// <summary>
    /// Manage the account data
    /// </summary>
    public class AccountController : BaseApiController
    {
        private readonly IGenericService<AccountBo> _accountService;
        private readonly IFilterHandler<AccountBo, AccountBoFilter> _listFilterHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountService"></param>
        /// <param name="listFilterHandler"></param>
        /// <param name="logger"></param>
        public AccountController(IGenericService<AccountBo> accountService, IFilterHandler<AccountBo, AccountBoFilter> listFilterHandler, ILogging logger) : base(logger)
        {
            _accountService = accountService;
            _listFilterHandler = listFilterHandler;
        }


        /// <summary>
        /// Return a list of all accounts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ApiResponse<AccountViewModel>))]
        [Route("api/account")]
        public async Task<IHttpActionResult> List([FromUri]AccountBoFilter filter)
        {
            var localFilter = _listFilterHandler.CreateFilter(filter);

            var result = await _accountService.ListAsync(localFilter);
            return Ok(result.ToApiResponse<AccountBo,AccountViewModel>());
        }

        /// <summary>
        /// Get one account
        /// </summary>
        /// <param name="id">The account id</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ApiResponse<AccountBo>))]
        [Route("api/account/{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var account = await _accountService.GetByIdAsync(id);
            LogResponse(account);
            return Ok(account.ToApiResponse<AccountBo,AccountViewModel>());
        }

        /// <summary>
        /// Add a new account
        /// </summary>
        /// <param name="account">The account to add</param>
        /// <returns>the Id of the new account</returns>
        [HttpPost]
        [ResponseType(typeof(ApiResponse<int>))]
        [Route("api/account")]
        public IHttpActionResult Add(AccountBo account)
        {
            var result = _accountService.AddOrUpdate(account, 0);
            return Ok(result.ToApiResponse());

        }

        /// <summary>
        /// Update an existing account
        /// </summary>
        /// <param name="account">The account to add</param>
        /// <returns>the Id of the new account</returns>
        [HttpPut]
        [ResponseType(typeof(ApiResponse<int>))]
        [Route("api/account")]
        public IHttpActionResult Update(AccountBo account)
        {
            var result = _accountService.AddOrUpdate(account, 0);
            return Ok(result.ToApiResponse());
        }

        /// <summary>
        /// Remove an existing account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ResponseType(typeof(ApiResponse<bool>))]
        public IHttpActionResult Delete(int id)
        {
            var result = _accountService.Delete(id, 0);
            return Ok(result.ToApiResponse());
        }



    }
}
