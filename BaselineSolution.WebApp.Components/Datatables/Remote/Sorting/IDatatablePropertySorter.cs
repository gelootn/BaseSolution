using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Sorting
{
    public interface IDatatablePropertySorter<TEntity>
    {
        IEntitySorter<TEntity> Sort(IEntitySorter<TEntity> sorter, SortDirection sortDirection);
    }
}
