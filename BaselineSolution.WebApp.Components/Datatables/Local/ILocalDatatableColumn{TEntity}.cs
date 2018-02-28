using BaselineSolution.WebApp.Components.Datatables.Base;

namespace BaselineSolution.WebApp.Components.Datatables.Local
{
    public interface ILocalDatatableColumn<TEntity>: IDatatableColumn<TEntity> where TEntity : class
    {
    }
}
