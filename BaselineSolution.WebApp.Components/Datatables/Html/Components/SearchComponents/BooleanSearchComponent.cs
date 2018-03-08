using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Base;
using HtmlBuilders;

namespace BaselineSolution.WebApp.Components.Datatables.Html.Components.SearchComponents
{
    public class BooleanSearchComponent: ISearchComponent
    {
        public IHtmlString ToHtml <TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class
        {
            return new HtmlTag("select").Attribute("name", column.Name)
                .Class("datatable-column-filter")
                .Class("form-control")
                .Append(new HtmlTag("option").Attribute("value", "").Append(Resources.WebApp.Form_All))
                .Append(new HtmlTag("option").Attribute("value", "null").Append(Resources.WebApp.Form_NotSet))
                .Append(new HtmlTag("option").Attribute("value", "true").Append(Resources.WebApp.Form_Yes))
                .Append(new HtmlTag("option").Attribute("value", "false").Append(Resources.WebApp.Form_No))
                .ToHtml();
        }
    }
}
