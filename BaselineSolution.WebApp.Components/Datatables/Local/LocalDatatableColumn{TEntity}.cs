using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.WebApp.Components.Datatables.Base;
using BaselineSolution.WebApp.Components.Datatables.Config;
using BaselineSolution.WebApp.Components.Datatables.Validation;

namespace BaselineSolution.WebApp.Components.Datatables.Local
{
    /// <summary>
    ///     Represents one property (column) in a datatable
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LocalDatatableColumn<TEntity> : DatatableColumn<TEntity>, ILocalDatatableColumn<TEntity> where TEntity : class
    {
        public LocalDatatableColumn(string header)
        {
            Searchable = true;
            Sortable = true;
            Visible = true;
            Header = header;
            Name = Guid.NewGuid().ToString();
            SearchComponent = DatatableConfiguration.Components.SearchComponents.Default;
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Enumerable.Empty<DatatableValidationResult>();
        }
    }
}