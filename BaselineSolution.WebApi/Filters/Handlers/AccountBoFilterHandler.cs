using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.WebApi.Filters.Account;
using BaselineSolution.WebApi.Infrastructure.Filters;

namespace BaselineSolution.WebApi.Filters.Handlers
{
    /// <summary>
    /// Account filter
    /// </summary>
    public class AccountBoFilterHandler : IFilterHandler<AccountBo, AccountBoFilter>
    {
        /// <inheritdoc />
        public IEntityFilter<AccountBo> CreateFilter(AccountBoFilter filter)
        {
            var localFilter = EntityFilter<AccountBo>.AsQueryable();
            if (!filter.Name.IsNullOrEmpty())
                localFilter = localFilter.Where(x => x.Name.Contains(filter.Name));

            return localFilter;
        }
    }
}