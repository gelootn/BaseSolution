using System;
using System.Web;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Components.Datatables.Html.Components.SearchComponents;

namespace BaselineSolution.WebApp.Components.Datatables.Local.Builder
{
    public interface ILocalDatatableColumnBuilder<TEntity>: ILocalDatatableBuilder<TEntity> where TEntity : class
    {
        ILocalDatatableColumnBuilder<TEntity> Display([NotNull] Func<TEntity, string> display);

        ILocalDatatableColumnBuilder<TEntity> Display([NotNull] Func<TEntity, IHtmlString> display);

        ILocalDatatableColumnBuilder<TEntity> Sortable(bool sortable);

        ILocalDatatableColumnBuilder<TEntity> Searchable(bool searchable);

        ILocalDatatableColumnBuilder<TEntity> Visible(bool visible);

        ILocalDatatableColumnBuilder<TEntity> Width([NotNull] string width);

        ILocalDatatableColumnBuilder<TEntity> Class([NotNull] string @class);

        ILocalDatatableColumnBuilder<TEntity> DefaultContent([NotNull] string defaultContent);

        ILocalDatatableColumnBuilder<TEntity> SearchComponent([NotNull] ISearchComponent searchComponent);
    }
}
