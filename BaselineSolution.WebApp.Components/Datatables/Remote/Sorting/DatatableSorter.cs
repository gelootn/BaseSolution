using System.Linq;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Sorting
{    
    /// <summary>
    /// Custom sorter that implements <see cref="Datatable"/> and provides sorting logic for a <see cref="Datatable"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DatatableSorter<TEntity>: IEntitySorter<TEntity> where TEntity : class
    {
        private IEntitySorter<TEntity> EntitySorter { get; set; }

        private RemoteDatatable<TEntity> Datatable { get; set; }
        private DatatableRequest Request { get; set; }

        private const string Ascending = "asc";
        private const string Descending = "desc";

        public DatatableSorter(IEntitySorter<TEntity> baseSorter, RemoteDatatable<TEntity> datatable, DatatableRequest request)
        {
            Request = request;
            Datatable = datatable;
            EntitySorter = baseSorter;
            MakeSorter();
        }

        private void MakeSorter()
        {
            for (int i = 0; i < Request.SortingColumnsCount; i++)
            {
                int sortingColumn = Request.SortingColumns[i];

                // If this column is not sortable, continue to the next
                if (!Request.Sortable[sortingColumn])
                    continue;
                
                // Lookup sortdirection, default to 'Ascending'
                SortDirection sortDirection;
                switch (Request.SortDirections[i])
                {
                    case Ascending:
                        sortDirection = SortDirection.Ascending;
                        break;
                    case Descending:
                        sortDirection = SortDirection.Descending;
                        break;
                    default:
                        sortDirection = SortDirection.Ascending;
                        break;
                }

                // Match property name to datatable column
                var name = Request.DataProperties[sortingColumn];
                var column = Datatable.Columns.Single(c => c.Name.Equals(name));

                // Add sorting rule to entitysorter
                EntitySorter = column.Sort(EntitySorter, sortDirection);
            }
            if (EntitySorter == null)
            {
                EntitySorter = Datatable.Columns.First(c => c.Sortable).Sort(null, SortDirection.Ascending);
            }
        }

        public IOrderedQueryable<TEntity> Sort(IQueryable<TEntity> entities)
        {
            return EntitySorter.Sort(entities);
        }

        public IEntitySorter<TCast> Cast<TCast>() where TCast : TEntity
        {
            return EntitySorter.Cast<TCast>();
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("EntitySorter: {0}", EntitySorter);
        }
    }
}
