using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Components.Datatables.Base;

namespace BaselineSolution.WebApp.Components.Datatables.Remote
{
    public interface IRemoteDatatableColumn<TEntity>: IDatatableColumn<TEntity> where TEntity : class
    {
        [NotNull]
        IEntitySorter<TEntity> Sort([CanBeNull] IEntitySorter<TEntity> sorter, SortDirection sortDirection);

        [NotNull]
        IEntityFilter<TEntity> Filter([CanBeNull] IEntityFilter<TEntity> filter, string search);
    }
}
