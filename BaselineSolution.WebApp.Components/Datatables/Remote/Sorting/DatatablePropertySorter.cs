using System;
using System.Linq.Expressions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Infrastructure.Sorting;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Sorting
{
    public class DatatablePropertySorter<TEntity, TProperty>: IDatatablePropertySorter<TEntity>
    {
        public Expression<Func<TEntity, TProperty>>  Property { get; set; }

        public DatatablePropertySorter(Expression<Func<TEntity, TProperty>> property)
        {
            Property = property;
        }

        public IEntitySorter<TEntity> Sort(IEntitySorter<TEntity> sorter, SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    return sorter == null ? EntitySorter<TEntity>.OrderBy(Property) : sorter.ThenBy(Property);
                case SortDirection.Descending:
                    return sorter == null ? EntitySorter<TEntity>.OrderByDescending(Property) : sorter.ThenByDescending(Property);
                default:
                    throw new ArgumentException("Unknown sort direction " + sortDirection);
            }
        }

        public override string ToString()
        {
            return string.Format("Property: {0}", Property);
        }
    }
}
