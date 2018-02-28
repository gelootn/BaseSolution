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
                .Append(new HtmlTag("option").Attribute("value", "").Append("-- Alle --"))
                .Append(new HtmlTag("option").Attribute("value", "null").Append("Niet opgegeven"))
                .Append(new HtmlTag("option").Attribute("value", "true").Append("Ja"))
                .Append(new HtmlTag("option").Attribute("value", "false").Append("Nee"))
                .ToHtml();
        }
    }
}
