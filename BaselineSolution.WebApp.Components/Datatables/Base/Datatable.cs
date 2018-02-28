using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Datatables.Validation;
using BaselineSolution.WebApp.Components.Models.Links;

namespace BaselineSolution.WebApp.Components.Datatables.Base
{
    /// <summary>
    ///     Represents a data table with columns
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class Datatable<TEntity> where TEntity : class
    {
        private ICollection<IDatatableColumn<TEntity>> _columns;
        private Func<TEntity, ILinkBuilder> _rowClickHandler;
        private Func<TEntity, string> _cssClass;


        public string Id { get; set; }
        protected HtmlHelper Helper { get; set; }

        public Datatable(HtmlHelper helper)
        {
            Helper = helper;
        }

        /// <summary>
        /// Gets or sets the table footer html string
        /// </summary>
        public virtual string Footer { get; set; }

        public virtual ICollection<IDatatableColumn<TEntity>> Columns
        {
            get { return _columns ?? (_columns = new List<IDatatableColumn<TEntity>>()); }
            set { _columns = value; }
        }

        public IEnumerable<DatatableValidationResult> Validate()
        {
            return InternalValidate();
        }

        protected abstract IEnumerable<DatatableValidationResult> InternalValidate();

        public override string ToString()
        {
            return string.Format("Type: {0}, Id: {1}, Columns: {2}", GetType(), Id, Columns);
        }

        public Datatable<TEntity> RowClickHandler(Func<TEntity, ILinkBuilder> value)
        {
            _rowClickHandler = value;
            return this;
        }

        public Datatable<TEntity> RowCssClass(Func<TEntity, string> value)
        {
            _cssClass = value;
            return this;
        }

        public Func<TEntity, ILinkBuilder>  RowClickHandler()
        {
            return _rowClickHandler;
        }

        public Func<TEntity, string> RowCssClass()
        {
            return _cssClass;
        }

        public abstract IHtmlString ToHtml(object htmlAttributes = null);


    }
}