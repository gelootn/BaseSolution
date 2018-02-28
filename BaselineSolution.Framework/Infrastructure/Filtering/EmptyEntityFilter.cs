using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.Framework.Infrastructure.Filtering
{
    /// <summary>An empty entity filter.</summary>
    [DebuggerDisplay("EntityFilter ( Unfiltered )")]
    internal sealed class EmptyEntityFilter<TEntity> : IEntityFilter<TEntity>
    {
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
            // We don't filter, but simply return the collection.
            return collection;
        }

        public IEnumerable<Expression<Func<TEntity, bool>>> Predicates
        {
            get { return Enumerable.Empty<Expression<Func<TEntity, bool>>>(); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCast"></typeparam>
        /// <returns></returns>
        public IEntityFilter<TCast> Cast<TCast>() where TCast : TEntity
        {
            return new EmptyEntityFilter<TCast>();
        }

        /// <summary>Returns an empty string.</summary>
        /// <returns>An empty string.</returns>
        public override string ToString()
        {
            return string.Empty;
        }
    }
}
