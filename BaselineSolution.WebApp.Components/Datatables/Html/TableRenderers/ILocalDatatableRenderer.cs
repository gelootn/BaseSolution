using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Local;

namespace BaselineSolution.WebApp.Components.Datatables.Html.TableRenderers
{
    public interface ILocalDatatableRenderer
    {
        IHtmlString Render<TEntity>(HtmlHelper htmlHelper, LocalDatatable<TEntity> datatable, object htmlAttributes) where TEntity : class;
    }
}
