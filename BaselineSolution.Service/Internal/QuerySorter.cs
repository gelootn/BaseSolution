using AutoMapper;
using AutoMapper.XpressionMapper.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Infrastructure.Sorting;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BaselineSolution.Service.Internal
{
    internal static class QuerySorter
    {
        public static IQueryable<TEntity> Sort<TBoEntity, TEntity>(this IQueryable<TEntity> query, IEntitySorter<TBoEntity> sorter)
        {
            if(sorter.GetExpressionType() == null)
            {
                return query;
            }

            var repoSorter = GetRepoSorter<TBoEntity, TEntity>(sorter);

            if (sorter.SortDirection == SortDirection.Ascending)
            {
                query = Queryable.OrderBy(query, (dynamic)repoSorter);
            }
            else
            {
                query = Queryable.OrderByDescending(query, (dynamic)repoSorter);
            }

            return query;
        }

        private static LambdaExpression GetRepoSorter<TBo, TEntity>(IEntitySorter<TBo> sorter)
        {
            LambdaExpression repoSorter = null;
            var sortExprType = sorter.GetExpressionType();

            if (sortExprType == typeof(int))
            { repoSorter = GetRepoSorter<TBo, TEntity, int>(sorter); }
            else if (sortExprType == typeof(int?))
            { repoSorter = GetRepoSorter<TBo, TEntity, int?>(sorter); }
            else if (sortExprType == typeof(string))
            { repoSorter = GetRepoSorter<TBo, TEntity, string>(sorter); }
            else if (sortExprType == typeof(DateTime))
            { repoSorter = GetRepoSorter<TBo, TEntity, DateTime>(sorter); }
            else if (sortExprType == typeof(decimal))
            { repoSorter = GetRepoSorter<TBo, TEntity, decimal>(sorter); }
            else if(sortExprType == typeof(char))
            { repoSorter = GetRepoSorter<TBo, TEntity, char>(sorter); }
            else if (sortExprType == typeof(byte))
            { repoSorter = GetRepoSorter<TBo, TEntity, byte>(sorter); }

            return repoSorter;
        }

        private static LambdaExpression GetRepoSorter<TBo, TEntity, T>(IEntitySorter<TBo> sorter)
        {
            return Mapper.Instance.MapExpression<Expression<Func<TBo, T>>, Expression<Func<TEntity, T>>>(sorter.GetExpression<T>()); ;
        }
    }
}
