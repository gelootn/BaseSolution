using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Remote;
using HtmlBuilders;

namespace BaselineSolution.WebApp.Components.Datatables.Html.TableRenderers
{
    public class TwitterBootstrapRemoteDatatableRenderer: IRemoteDatatableRenderer
    {
        public IHtmlString Render <TEntity>(HtmlHelper htmlHelper, RemoteDatatable<TEntity> remoteDatatable, object htmlAttributes) where TEntity : class 
        {
            // Build HTML

            // table
            var table = new HtmlTag("table").Id(remoteDatatable.Id)
                .Class("enable-remote-datatable")
                .Data("url", remoteDatatable.Url)
                .Class("table table-striped");


            // thead
            var thead = new HtmlTag("thead");

            // column filters

            if (remoteDatatable.RowClickHandler() != null)
            {
                var column = new RemoteDatatableColumn<TEntity>(string.Empty)
                {
                    DisplayFunction = x => remoteDatatable.RowClickHandler()(x).Link,
                    Visible = false,
                    Name = "Column" + remoteDatatable.Columns.Count,
                };

                remoteDatatable.Columns.Add(column);

                table.Data("row-click-handler-column-name", column.Name);
            }

            if (remoteDatatable.RowCssClass() != null)
            {
                var column = new RemoteDatatableColumn<TEntity>(string.Empty)
                {
                    DisplayFunction = x => remoteDatatable.RowCssClass()(x),
                    Visible = false,
                    Name = "Column" + remoteDatatable.Columns.Count
                };

                remoteDatatable.Columns.Add(column);

                table.Data("row-css-class-column-name", column.Name);

            }


            var trColumnFilters = new HtmlTag("tr").Class("datatable-column-filters");

            

            foreach (var column in remoteDatatable.Columns)
            {
                // don't render the search component if this column is not searchable
                if (!column.Searchable)
                {
                    trColumnFilters.Append(new HtmlTag("td"));
                }
                else
                {
                    var controls = new HtmlTag("div").Class("controls");
                    controls.Append(HtmlTag.ParseAll(column.SearchComponent.ToHtml(htmlHelper, column)));
                    trColumnFilters.Append(new HtmlTag("td").Append(controls));
                }
            }

            // column headers

            var trColumnHeaders = new HtmlTag("tr").Class("datatable-column-headers");
            foreach (var column in remoteDatatable.Columns)
            {
                var th = new HtmlTag("th")
                    .Data("property", column.Name)
                    .Append(column.Header)
                    .Merge(column.GetAttributes());
                trColumnHeaders.Append(th);
            }

            thead.Append(trColumnFilters);
            thead.Append(trColumnHeaders);


            //tbody
            var tbody = new HtmlTag("tbody");



            table.Merge(htmlAttributes)
                .Append(thead)
                .Append(tbody);

            return table.ToHtml();
        }
    }
}
