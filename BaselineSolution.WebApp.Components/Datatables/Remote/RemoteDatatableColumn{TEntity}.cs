using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Components.Datatables.Base;
using BaselineSolution.WebApp.Components.Datatables.Config;
using BaselineSolution.WebApp.Components.Datatables.Remote.Filtering;
using BaselineSolution.WebApp.Components.Datatables.Remote.Sorting;
using BaselineSolution.WebApp.Components.Datatables.Validation;

namespace BaselineSolution.WebApp.Components.Datatables.Remote
{
    /// <summary>
    /// Represents a column in an instance of <see cref="RemoteDatatable{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity</typeparam>
    public class RemoteDatatableColumn<TEntity> : DatatableColumn<TEntity>, IRemoteDatatableColumn<TEntity> where TEntity : class 
    {
        private ICollection<IDatatablePropertySorter<TEntity>> _propertySorters;

        public ICollection<IDatatablePropertySorter<TEntity>> PropertySorters
        {
            get { return _propertySorters ?? (_propertySorters = new List<IDatatablePropertySorter<TEntity>>()); }
            set { _propertySorters = value; }
        }

        public IDatatablePropertyFilter<TEntity> PropertyFilter { get; set; }

        public RemoteDatatableColumn(string header)
        {
            Header = header;
            Name = Guid.NewGuid().ToString();
            Sortable = false;
            Searchable = false;
            Visible = true;
            SearchComponent = DatatableConfiguration.Components.SearchComponents.Default;
        }

        public IEntitySorter<TEntity> Sort(IEntitySorter<TEntity> sorter, SortDirection sortDirection)
        {
            return PropertySorters.Aggregate(sorter, (current, propertySorter) => propertySorter.Sort(current, sortDirection));
        }

        public IEntityFilter<TEntity> Filter(IEntityFilter<TEntity> filter, string search)
        {
            if (!Searchable)
                return filter;
            return PropertyFilter.Filter(filter, search);
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            if(Searchable && PropertyFilter == null)
                yield return new DatatableValidationResult(Description + " does not have a property filter, even though it is configured to be searchable (it's impossible to apply filtering to this column without a property filter)");
            if(Sortable && (PropertySorters == null || !PropertySorters.Any()))
                yield return new DatatableValidationResult(Description + " does not have any property sorters, even though it is configured to be sortable! (it's impossible to apply sorting to this column without at least 1 property sorter)");
        }

        public override string ToString()
        {
            return string.Format("{0}, PropertyFilter: {1}, PropertySorters: {2}", base.ToString(), PropertyFilter, PropertySorters);
        }
    }
}