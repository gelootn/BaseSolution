using BaselineSolution.WebApp.Components.Datatables.Base;

namespace BaselineSolution.WebApp.Components.Datatables.Remote
{
    public interface IRemoteDatatableColumn<TEntity, TProperty>: IRemoteDatatableColumn<TEntity>, IDatatableColumn<TEntity, TProperty> where TEntity : class
    {
    }
}
