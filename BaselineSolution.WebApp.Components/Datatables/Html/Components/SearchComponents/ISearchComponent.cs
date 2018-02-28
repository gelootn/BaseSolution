using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Base;

namespace BaselineSolution.WebApp.Components.Datatables.Html.Components.SearchComponents
{
    public interface ISearchComponent
    {
        IHtmlString ToHtml<TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class;
    }
}
