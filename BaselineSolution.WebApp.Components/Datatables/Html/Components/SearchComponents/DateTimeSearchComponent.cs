using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Base;
using HtmlBuilders;

namespace BaselineSolution.WebApp.Components.Datatables.Html.Components.SearchComponents
{
    public class DateTimeSearchComponent: ISearchComponent
    {
        public IHtmlString ToHtml <TEntity>(HtmlHelper htmlHelper, IDatatableColumn<TEntity> column) where TEntity : class
        {
            var controls = HtmlTags.Div.Class("controls")
                .Append(HtmlTags.Div.Class("input-prepend input-addon")
                    .Append(HtmlTags.Span.Class("add-on")
                        .Append(HtmlTags.I.Class("icon-search")))
                    .Append(HtmlTags.Input.DateTime
                        .Class("datatable-column-filter")
                        .Name(column.Name.Replace(" ", "."))
                        .Attribute("placeholder", column.Header)
                        .Render(TagRenderMode.SelfClosing)));

            return controls.ToHtml();
        }
    }
}
