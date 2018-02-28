using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Local;
using HtmlBuilders;

namespace BaselineSolution.WebApp.Components.Datatables.Html.TableRenderers
{
    public class TwitterBootstrapLocalDatatableRenderer : ILocalDatatableRenderer
    {
        public IHtmlString Render<TEntity>(HtmlHelper htmlHelper, LocalDatatable<TEntity> localDatatable, object htmlAttributes) where TEntity : class
        {
            // Build HTML

            // table
            var table = HtmlTags.Table.Id(localDatatable.Id)
                .Class("table")
                .Class("table-striped")
                .Class("enable-local-datatable");



            // thead
            var thead = new HtmlTag("thead");

            // column filters

            if (localDatatable.RowClickHandler() != null)
            {
                var column = new LocalDatatableColumn<TEntity>(string.Empty)
                {
                    DisplayFunction = x => localDatatable.RowClickHandler()(x).Link,
                    Visible = false,
                    Name = "Column" + localDatatable.Columns.Count,
                };

                localDatatable.Columns.Add(column);

                table.Data("row-click-handler-column-name", column.Name);
            }

            var trColumnFilters = HtmlTags.Tr.Class("datatable-column-filters");
            foreach (var column in localDatatable.Columns)
            {
                // don't render the search component if this column is not searchable
                if (!column.Searchable)
                {
                    trColumnFilters.Append(new HtmlTag("td"));
                }
                else
                {
                    var controls = new HtmlTag("div").Class("controls");
                    controls.Append(HtmlTag.ParseAll(column.SearchComponent.ToHtml(htmlHelper, column).ToHtmlString()));
                    trColumnFilters.Append(new HtmlTag("td").Append(controls));
                }
            }

            // column headers

            var trColumnHeaders = new HtmlTag("tr").Class("datatable-column-headers");
            foreach (var column in localDatatable.Columns)
            {
                var th = new HtmlTag("th")
                    .Attribute("data-property", column.Name)
                    .Append(column.Header)
                    .Merge(column.GetAttributes());
                trColumnHeaders.Append(th);
            }

            thead.Append(trColumnFilters);
            thead.Append(trColumnHeaders);


            //tbody
            var tbody = new HtmlTag("tbody");

            foreach (var entity in localDatatable.Entities)
            {

                var tbodyrow = new HtmlTag("tr");
                if (localDatatable.RowCssClass() != null)
                {
                    tbodyrow.Class(
                        localDatatable.RowCssClass()
                            .Invoke(entity));
                }

                foreach (var column in localDatatable.Columns)
                {
                    tbodyrow.Append(new HtmlTag("td").Append(column.DisplayFunction(entity)));
                }
                tbody.Append(tbodyrow);
            }


            //tfoot
            var tfoot = new HtmlTag("tfoot");
            tfoot.Append(localDatatable.Footer ?? string.Empty);

            table.Merge(htmlAttributes)
                .Append(thead)
                .Append(tbody)
                .Append(tfoot);


            return table.ToHtml();
        }
    }
}
