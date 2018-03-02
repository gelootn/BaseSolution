using AutoMapper;
using AutoMapper.XpressionMapper.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using LinqKit;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BaselineSolution.Service.Internal
{
    internal static class QueryFilter
    {
        public static IQueryable<TEntity> Filter<TBoEntity, TEntity>(this IQueryable<TEntity> query, IEntityFilter<TBoEntity> filters)
        {
            var repofilter = filters.GetRepoFilter<TBoEntity, TEntity>();
            return query.Where(repofilter);
        }

        public static Expression<Func<TEntity, bool>> GetRepoFilter<TBo, TEntity>(this IEntityFilter<TBo> filters)
        {
            Expression<Func<TEntity, bool>> repofilter = user => true;
            if (filters == null)
            {
                return repofilter;
            }

            foreach (var filter in filters.Predicates)
            {
                var filterExp = Mapper.Instance.MapExpression<Expression<Func<TBo, bool>>, Expression<Func<TEntity, bool>>>(filter);
                repofilter = repofilter.And(filterExp);
            }            

            return repofilter;
        }        
    }
}
