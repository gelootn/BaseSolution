using BaselineSolution.Bo.Internal;
using BaselineSolution.Framework.Infrastructure;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.WebApi.Infrastructure.Filters
{
    /// <summary>
    /// Transform the API filter to a <see cref="IEntityFilter{TEntity}"/>
    /// </summary>
    /// <typeparam name="TBo">Business object</typeparam>
    /// <typeparam name="TFilter">Business object filter</typeparam>
    public interface IFilterHandler<TBo, in TFilter>
        where TBo : BaseBo
    {
        /// <summary>
        /// Create a <see cref="IEntityFilter{TEntity}"/> from the given API filter
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEntityFilter<TBo> CreateFilter(TFilter filter);
    }
}