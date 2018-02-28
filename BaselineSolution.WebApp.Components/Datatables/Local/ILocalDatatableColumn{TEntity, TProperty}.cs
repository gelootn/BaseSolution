using BaselineSolution.WebApp.Components.Datatables.Base;

namespace BaselineSolution.WebApp.Components.Datatables.Local
{
    public interface ILocalDatatableColumn<TEntity, TProperty>: IDatatableColumn<TEntity, TProperty> where TEntity : class
    {
    }
}
