using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using BaselineSolution.WebApp.Components.Datatables.Html.Components.SearchComponents;

namespace BaselineSolution.WebApp.Components.Datatables.Local.Builder
{
    public class LocalDatatableColumnBuilder<TEntity>: ILocalDatatableColumnBuilder<TEntity> where TEntity : class
    {
        private readonly ILocalDatatableBuilder<TEntity> _localDatatableBuilder;
        private readonly ILocalDatatableColumn<TEntity> _localDatatableColumn; 

        public LocalDatatableColumnBuilder(ILocalDatatableBuilder<TEntity> localDatatableBuilder, ILocalDatatableColumn<TEntity> localDatatableColumn)
        {
            _localDatatableBuilder = localDatatableBuilder;
            _localDatatableColumn = localDatatableColumn;
        }

        

        public ILocalDatatableColumnBuilder<TEntity> Column(string header)
        {
            return _localDatatableBuilder.Column(header);
        }

        public ILocalDatatableColumnBuilder<TEntity, bool?> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, int?> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, double?> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, decimal?> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, long?> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, short?> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, string> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, DateTime?> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, TimeSpan?> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public ILocalDatatableColumnBuilder<TEntity, ICollection<TNewProperty>> Column<TNewProperty>(string header, Expression<Func<TEntity, ICollection<TNewProperty>>> propertyExpression)
        {
            return _localDatatableBuilder.Column(header, propertyExpression);
        }

        public LocalDatatable<TEntity> Build()
        {
            return _localDatatableBuilder.Build();
        }

        public LocalDatatable<TEntity> Build(string htmlString)
        {
            return _localDatatableBuilder.Build(htmlString);
        }

        public ILocalDatatableColumnBuilder<TEntity> Display(Func<TEntity, string> display)
        {
            _localDatatableColumn.DisplayFunction = display;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Display(Func<TEntity, IHtmlString> display)
        {
            _localDatatableColumn.DisplayFunction = entity => display(entity).ToHtmlString();
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Sortable(bool sortable)
        {
            _localDatatableColumn.Sortable = sortable;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Searchable(bool searchable)
        {
            _localDatatableColumn.Searchable = searchable;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Visible(bool visible)
        {
            _localDatatableColumn.Visible = visible;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Width(string width)
        {
            _localDatatableColumn.Width = width;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> Class(string @class)
        {
            _localDatatableColumn.Class = @class;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> DefaultContent(string defaultContent)
        {
            _localDatatableColumn.DefaultContent = defaultContent;
            return this;
        }

        public ILocalDatatableColumnBuilder<TEntity> SearchComponent(ISearchComponent searchComponent)
        {
            _localDatatableColumn.SearchComponent = searchComponent;
            return this;
        }
    }
}
