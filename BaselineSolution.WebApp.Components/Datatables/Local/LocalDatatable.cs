using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Base;
using BaselineSolution.WebApp.Components.Datatables.Config;
using BaselineSolution.WebApp.Components.Datatables.Validation;

namespace BaselineSolution.WebApp.Components.Datatables.Local
{
    /// <summary>
    ///     Represents a data table of which all the data is loaded immediately.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class LocalDatatable<TEntity> : Datatable<TEntity> where TEntity : class
    {
        public LocalDatatable(HtmlHelper helper) : base(helper)
        {
        }

        public IEnumerable<TEntity> Entities { get; set; }


        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Columns.SelectMany(c => c.Validate());
        }

        public override string ToString()
        {
            return string.Format("{0}, Entities: {1}", base.ToString(), Entities.Count());
        }

        public override IHtmlString ToHtml(object htmlAttributes = null)
        {
            return DatatableConfiguration.TableRenderers.LocalDatatableRenderer.Render(Helper, this, htmlAttributes);
        }

        
    }
}