﻿using BaselineSolution.Framework.Infrastructure.Sorting;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BaselineSolution.Framework.Infrastructure.Contracts
{
    /// <summary>
    ///     Defines a <see cref="Sort" /> method that enables sorting of a specified collection.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    public interface IEntitySorter<TEntity>
    {
        /// <summary>
        ///     Sorts the specified collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TEntity}" /> whose elements are sorted according to the
        ///     <see cref="ArgumentNullException" />.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="collection" /> is a null
        ///     reference.
        /// </exception>
        IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> collection);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCast"></typeparam>
        /// <returns></returns>
        IEntitySorter<TCast> Cast<TCast>() where TCast : TEntity;

        Expression<Func<TEntity, TKey>> GetExpression<TKey>();
        Type GetExpressionType();
        SortDirection SortDirection { get; set; }
    }
}
