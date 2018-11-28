using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.WebApi.Filters.Account;
using BaselineSolution.WebApi.Infrastructure.Filters;

namespace BaselineSolution.WebApi.Filters.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class RoleBoFilterHandler : IFilterHandler<RoleBo, RoleBoFilter>
    {
        /// <inheritdoc />
        public IEntityFilter<RoleBo> CreateFilter(RoleBoFilter filter)
        {
            var localFilter = EntityFilter<RoleBo>.AsQueryable();

            if (!filter.Name.IsNullOrEmpty())
                localFilter = localFilter.Where(x => x.Name.Contains(filter.Name));

            return localFilter;

        }
    }
}