using System.Web;
using BaselineSolution.WebApp.Components.Datatables.Base;

namespace BaselineSolution.WebApp.Components.Datatables.Html.Components.DisplayComponents
{
    public interface IDisplayComponent<TProperty>
    {
        IHtmlString ToHtml<TEntity>(TEntity entity, IDatatableColumn<TEntity, TProperty> datatableColumn) where TEntity : class;
    }
}
