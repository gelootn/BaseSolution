using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Remote;

namespace BaselineSolution.WebApp.Components.Datatables.Html.TableRenderers
{
    public interface IRemoteDatatableRenderer
    {
        IHtmlString Render<TEntity>(HtmlHelper htmlHelper, RemoteDatatable<TEntity> remoteDatatable, object htmlAttributes) where TEntity : class ;
    }
}
