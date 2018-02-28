using System;
using System.Web;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Components.Datatables.Html.Components.DisplayComponents;
using BaselineSolution.WebApp.Components.Datatables.Html.Components.SearchComponents;

namespace BaselineSolution.WebApp.Components.Datatables.Local.Builder
{
    public interface ILocalDatatableColumnBuilder<TEntity, TProperty>: ILocalDatatableBuilder<TEntity> where TEntity : class
    {
        ILocalDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] Func<TEntity, string> display);

        ILocalDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] Func<TEntity, IHtmlString> display);

        ILocalDatatableColumnBuilder<TEntity, TProperty> Display([NotNull] IDisplayComponent<TProperty> displayComponent);

        ILocalDatatableColumnBuilder<TEntity, TProperty> Sortable(bool sortable);

        ILocalDatatableColumnBuilder<TEntity, TProperty> Searchable(bool searchable);

        ILocalDatatableColumnBuilder<TEntity, TProperty> Visible(bool visible);

        ILocalDatatableColumnBuilder<TEntity, TProperty> Width([NotNull] string width);

        ILocalDatatableColumnBuilder<TEntity, TProperty> Class([NotNull] string @class);

        ILocalDatatableColumnBuilder<TEntity, TProperty> DefaultContent([NotNull] string defaultContent);

        ILocalDatatableColumnBuilder<TEntity, TProperty> SearchComponent([NotNull] ISearchComponent searchComponent);
    }
}
