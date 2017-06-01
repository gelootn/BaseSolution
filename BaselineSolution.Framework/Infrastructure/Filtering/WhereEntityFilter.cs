using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using LinqKit;

namespace BaselineSolution.Framework.Infrastructure.Filtering
{
    /// <summary>
    ///     Filters the collection using a predicate.
    /// </summary>
    /// <typeparam name="TEntity">
    ///     The type of the entity.
    /// </typeparam>
    [DebuggerDisplay("EntityFilter ( where {ToString()} )")]
    internal sealed class WhereEntityFilter<TEntity> : IEntityFilter<TEntity>
    {
        /// <summary>
        ///     The _base filter.
        /// </summary>
        private readonly IEntityFilter<TEntity> _baseFilter;

        /// <summary>
        ///     The _predicate.
        /// </summary>
        private readonly Expression<Func<TEntity, bool>> _predicate;

        /// <summary>
        ///     Initializes a new instance of the <see cref="WhereEntityFilter{TEntity}" /> class.
        /// </summary>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        public WhereEntityFilter(Expression<Func<TEntity, bool>> predicate)
        {
            _predicate = predicate;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="WhereEntityFilter{TEntity}" /> class.
        /// </summary>
        /// <param name="baseFilter">
        ///     The base filter.
        /// </param>
        /// <param name="predicate">
        ///     The predicate.
        /// </param>
        public WhereEntityFilter(IEntityFilter<TEntity> baseFilter, Expression<Func<TEntity, bool>> predicate)
        {
            _baseFilter = baseFilter;
            _predicate = predicate;
        }

        /// <summary>
        ///     Filters the specified collection.
        /// </summary>
        /// <param name="collection">
        ///     The collection.
        /// </param>
        /// <returns>
        ///     A filtered collection.
        /// </returns>
        public IQueryable<TEntity> Filter(IQueryable<TEntity> collection)
        {
            var expanded = _predicate.Expand();
            if (_baseFilter == null)
            {

                return collection.AsExpandable().Where(expanded);
            }
            return _baseFilter.Filter(collection).Where(expanded);
        }

        public IEnumerable<Expression<Func<TEntity, bool>>> Predicates
        {
            get
            {
                if (_baseFilter != null)
                {
                    foreach (var predicate in _baseFilter.Predicates)
                        yield return predicate;
                }
                yield return _predicate;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCast"></typeparam>
        /// <returns></returns>
        public IEntityFilter<TCast> Cast<TCast>() where TCast : TEntity
        {
            if (_baseFilter != null)
            {
                return new WhereEntityFilter<TCast>(_baseFilter.Cast<TCast>(), _predicate.Cast<TEntity, bool, TCast>());
            }
            return new WhereEntityFilter<TCast>(_predicate.Cast<TEntity, bool, TCast>());
        }



        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            string baseFilterPresentation = _baseFilter != null ? _baseFilter.ToString() : string.Empty;

            // The returned string is used in de DebuggerDisplay.
            if (!string.IsNullOrEmpty(baseFilterPresentation))
            {
                return baseFilterPresentation + ", " + _predicate;
            }

            return _predicate.ToString();
        }
    }
}
