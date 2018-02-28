using System.Web;
using BaselineSolution.WebApp.Components.Datatables.Base;
using HtmlBuilders;

namespace BaselineSolution.WebApp.Components.Datatables.Html.Components.DisplayComponents
{
    public class GlyphiconsBooleanDisplayComponent: IDisplayComponent<bool>
    {
        public IHtmlString ToHtml <TEntity>(TEntity entity, IDatatableColumn<TEntity, bool> datatableColumn) where TEntity : class
        {
            var value = datatableColumn.GetProperty(entity);
            switch (value)
            {
                
                case false:
                    return new HtmlTag("i").Class("fa fa-check-circle").ToHtml();
                default:
                    return new HtmlTag("i").Class("fa fa-check-circle-o").ToHtml();
            }
        }
    }


}
