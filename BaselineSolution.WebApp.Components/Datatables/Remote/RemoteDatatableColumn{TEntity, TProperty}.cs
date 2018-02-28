using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Infrastructure.Sorting;
using BaselineSolution.WebApp.Components.Datatables.Base;
using BaselineSolution.WebApp.Components.Datatables.Config;
using BaselineSolution.WebApp.Components.Datatables.Remote.Filtering;
using BaselineSolution.WebApp.Components.Datatables.Validation;

namespace BaselineSolution.WebApp.Components.Datatables.Remote
{
    public class RemoteDatatableColumn<TEntity, TProperty>: DatatableColumn<TEntity, TProperty>, IRemoteDatatableColumn<TEntity, TProperty> where TEntity : class
    {
        public IDatatablePropertyFilter<TEntity> PropertyFilter { get; set; }

        public RemoteDatatableColumn(string header, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            Header = header;
            Sortable = true;
            Searchable = true;
            Visible = true;
            PropertyExpression = propertyExpression;
            Name = propertyExpression.GetPropertyName().Replace(".", string.Empty);
            if(string.IsNullOrEmpty(Name))
                Name = Guid.NewGuid().ToString();
            var displayComponent = DatatableConfiguration.Components.DisplayComponents.Lookup<TProperty>();
            if (displayComponent == null)
            {
                var propertyFunction = propertyExpression.Compile();
                DisplayFunction = entity => Convert.ToString(propertyFunction(entity));
            }
            else
            {
                DisplayFunction = entity => displayComponent.ToHtml(entity, this).ToHtmlString();
            }
            SearchComponent = DatatableConfiguration.Components.SearchComponents.Lookup<TProperty>();
        }

        public IEntitySorter<TEntity> Sort(IEntitySorter<TEntity> sorter, SortDirection sortDirection)
        {
            if (!Sortable)
                return sorter;
            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    return sorter == null
                        ? EntitySorter<TEntity>.OrderBy(PropertyExpression)
                        : sorter.ThenBy(PropertyExpression);
                case SortDirection.Descending:
                    return sorter == null
                        ? EntitySorter<TEntity>.OrderByDescending(PropertyExpression)
                        : sorter.ThenByDescending(PropertyExpression);
                default:
                    throw new ArgumentException("Invalid sortDirection: " + sortDirection);
            }
        }

        public IEntityFilter<TEntity> Filter(IEntityFilter<TEntity> filter, string search)
        {
            if (!Searchable)
                return filter;
            return PropertyFilter.Filter(filter, search);
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            if (Searchable && PropertyFilter == null)
                yield return new DatatableValidationResult(Description + " does not have a property filter, even though it is configured to be searchable (it's impossible to apply filtering to this column without a property filter)");
        }

        public override string ToString()
        {
            return string.Format("{0}, PropertyFilter: {1}", base.ToString(), PropertyFilter);
        }
    }
}
