using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Base;
using BaselineSolution.WebApp.Components.Datatables.Config;
using BaselineSolution.WebApp.Components.Datatables.Validation;
using HtmlBuilders;

namespace BaselineSolution.WebApp.Components.Datatables.Remote
{
    /// <summary>
    /// Represents a remote datatable, of which the data can be fetched through the <see cref="Url"/>
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity for which this datatable was made</typeparam>
    public class RemoteDatatable<TEntity> : Datatable<TEntity> where TEntity : class
    {
        
        private ICollection<IRemoteDatatableColumn<TEntity>> _columns;
       

        public RemoteDatatable(HtmlHelper helper):base(helper)
        {
        }

        /// <summary>
        /// Gets or sets the url where the data can be fetched from
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     Gets or sets the columns
        /// </summary>
        public new virtual ICollection<IRemoteDatatableColumn<TEntity>> Columns
        {
            get { return _columns ?? (_columns = new List<IRemoteDatatableColumn<TEntity>>()); }
            set { _columns = value; }
        }

        protected override IEnumerable<DatatableValidationResult> InternalValidate()
        {
            return Columns.SelectMany(column => column.Validate());
        }

        public override string ToString()
        {
            return string.Format("{0}, Url: {1}, Columns: {2}", base.ToString(), Url, Columns);
        }


        public override IHtmlString ToHtml(object htmlAttributes = null)
        {
            var validationResults = Validate().ToList();
            if (validationResults.Any())
            {
                var div = new HtmlTag("div").Class("warning");
                var validationList = new HtmlTag("ul");
                foreach (var validationResult in validationResults)
                    validationList.Append(new HtmlTag("li").Append(validationResult.Message));
                return div.Append(validationList).ToHtml();
            }

            // Store datatable in storage
            DatatableStorage.Put(this);

            // Build HTML
            return DatatableConfiguration.TableRenderers.RemoteDatatableRenderer.Render(Helper, this, htmlAttributes);
        }


    }
}
