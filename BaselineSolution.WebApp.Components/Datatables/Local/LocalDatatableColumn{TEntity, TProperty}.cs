using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.WebApp.Components.Datatables.Base;
using BaselineSolution.WebApp.Components.Datatables.Config;
using BaselineSolution.WebApp.Components.Datatables.Validation;

namespace BaselineSolution.WebApp.Components.Datatables.Local
{
    public class LocalDatatableColumn<TEntity, TProperty>: DatatableColumn<TEntity, TProperty>, ILocalDatatableColumn<TEntity, TProperty> where TEntity : class
    {
        public LocalDatatableColumn(string header, Expression<Func<TEntity, TProperty>> propertyExpression)
        {
            Searchable = true;
            Sortable = true;
            Visible = true;
            PropertyExpression = propertyExpression;
            Header = header;
            Name = propertyExpression.GetPropertyName().Replace(".", string.Empty);
            if (string.IsNullOrEmpty(Name))
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

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Enumerable.Empty<DatatableValidationResult>();
        }
    }
}
