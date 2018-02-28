using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;

namespace BaselineSolution.Framework.Infrastructure.Including
{
    public class PropertyInclusionHolder<TEntity, TProperty>: IPropertyInclusionHolder<TEntity> where TEntity : class
    {
        private Expression<Func<TEntity, TProperty>> _propertyExpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyInclusionHolder{TEntity,TProperty}"/> class.
        /// </summary>
        public PropertyInclusionHolder(Expression<Func<TEntity, TProperty>> property)
        {
            _propertyExpression = property;
        }

        /// <summary>
        ///     Adds one property inclusion to the <paramref name="entities"/>
        /// </summary>
        /// <param name="entities">The entities</param>
        /// <returns>The entities that are now marked with the property inclusion</returns>
        public IQueryable<TEntity> AddPropertyInclusion(IQueryable<TEntity> entities)
        {
            return entities.Include(_propertyExpression);
        }

        public IPropertyInclusionHolder<TCast> Cast<TCast>() where TCast : class, TEntity
        {
            Expression<Func<TEntity, TProperty>> property = _propertyExpression;
            Expression<Func<TCast, TProperty>> expression = cast => property.Invoke(cast);
            return new PropertyInclusionHolder<TCast, TProperty>(expression.Expand());
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}", _propertyExpression.Body);
        }
    }
}
