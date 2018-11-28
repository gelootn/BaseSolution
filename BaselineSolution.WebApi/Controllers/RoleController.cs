﻿using BaselineSolution.Bo.Models.Security;
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

namespace BaselineSolution.WebApi.Controllers
{
    /// <summary>
    /// Manage the Role Data
    /// </summary>
    public class RoleController : BaseApiController
    {
        private readonly IGenericService<RoleBo> _roleService;
        private readonly IFilterHandler<RoleBo, RoleBoFilter> _filterHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleService"></param>
        /// <param name="filterHandler"></param>
        /// <param name="logging"></param>
        public RoleController(IGenericService<RoleBo> roleService, IFilterHandler<RoleBo, RoleBoFilter> filterHandler, ILogging logging) : base(logging)
        {
            _roleService = roleService;
            _filterHandler = filterHandler;
        }


        /// <summary>
        /// Get a list of all roles
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ApiResponse<RoleBo>))]
        [Route("api/role")]
        public async Task<IHttpActionResult> List([FromUri]RoleBoFilter filter)
        {
            var localFilter = _filterHandler.CreateFilter(filter);
            var result = await _roleService.ListAsync(localFilter);
            return Ok(result);
        }

        /// <summary>
        /// Get one role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ApiResponse<RoleBo>))]
        [Route("api/role/{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _roleService.GetByIdAsync(id);
            return Ok(result.ToApiResponse());
        }

        /// <summary>
        /// Add a new role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(ApiResponse<int>))]
        [Route("api/role")]
        public IHttpActionResult Add(RoleBo role)
        {
            var response = _roleService.AddOrUpdate(role, 0);
            return Ok(response.ToApiResponse());
        }


        /// <summary>
        /// Update an existing role
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut]
        [ResponseType(typeof(ApiResponse<int>))]
        [Route("api/role")]
        public IHttpActionResult Update(RoleBo role)
        {
            var response = _roleService.AddOrUpdate(role, 0);
            return Ok(response.ToApiResponse());
        }

    }
}