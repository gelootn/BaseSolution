using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Filtering
{
    public interface IDatatablePropertyFilter<TEntity>
    {
        IEntityFilter<TEntity> Filter(IEntityFilter<TEntity> filter, string search);
    }
}
