﻿using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.Framework.Infrastructure.Sorting
{
    /// <summary>
    ///     Defines an EntitySorter for the ThenBy clause.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    /// <typeparam name="TKey">
    ///     The type of the key.
    /// </typeparam>
    [DebuggerDisplay("EntitySorter ( OrderBy: {ToString()})")]
    internal sealed class ThenByEntitySorter <TEntity, TKey> : IEntitySorter<TEntity>
    {
        /// <summary>
        ///     The _base sorter.
        /// </summary>
        private readonly IEntitySorter<TEntity> _baseSorter;

        /// <summary>
        ///     The _sortDirection.
        /// </summary>
        private SortDirection _sortDirection;

        /// <summary>
        ///     The _key selector.
        /// </summary>
        private readonly Expression<Func<TEntity, TKey>> _keySelector;

        public SortDirection SortDirection { get => _sortDirection; set => _sortDirection = value; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThenByEntitySorter{TEntity,TKey}" /> class.
        ///     Initializes a new instance of the <see cref="ThenByEntitySorter{TEntity, TKey}" /> class.
        /// </summary>
        /// <param name="baseSorter">
        ///     The base sorter.
        /// </param>
        /// <param name="keySelector">
        ///     The key selector.
        /// </param>
        /// <param name="sortDirection">
        ///     The sortDirection.
        /// </param>
        public ThenByEntitySorter(
            IEntitySorter<TEntity> baseSorter, Expression<Func<TEntity, TKey>> keySelector, SortDirection sortDirection)
        {
            _baseSorter = baseSorter;
            _keySelector = keySelector;
            _sortDirection = sortDirection;
        }

        /// <summary>
        ///     Sorts the specified collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     An <see cref="IOrderedEnumerable{TEntity}" />.
        /// </returns>
        public IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> collection)
        {
            IOrderedQueryable<TEntity> sortedCollection = _baseSorter.Sort(collection);

            if (_sortDirection == SortDirection.Ascending)
            {
                return sortedCollection.ThenBy(_keySelector);
            }

            return sortedCollection.ThenByDescending(_keySelector);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCast"></typeparam>
        /// <returns></returns>
        public IEntitySorter<TCast> Cast<TCast>() where TCast : TEntity
        {
            return new ThenByEntitySorter<TCast, TKey>(_baseSorter.Cast<TCast>(), _keySelector.Cast<TEntity, TKey, TCast>(), _sortDirection);
        }

        /// <summary>Returns a String that represents the current object.</summary>
        /// <returns>A string representing the object.</returns>
        public override string ToString()
        {
            string sortType = _sortDirection == SortDirection.Ascending ? string.Empty : " descending";

            return _baseSorter + ", " + _keySelector + sortType;
        }

        public Expression<Func<TEntity, TKey1>> GetExpression<TKey1>()
        {
            return (Expression<Func<TEntity, TKey1>>)Convert.ChangeType(_keySelector, typeof(Expression<Func<TEntity, TKey1>>));
        }

        public Type GetExpressionType()
        {
            return typeof(TKey);
        }
    }
}