using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Builder
{
    public abstract class RemoteDatatableColumnBuilderBase<TEntity>: IRemoteDatatableBuilder<TEntity> where TEntity : class 
    {
        private readonly IRemoteDatatableBuilder<TEntity> _remoteDatatableBuilder;

        protected RemoteDatatableColumnBuilderBase(IRemoteDatatableBuilder<TEntity> remoteDatatableBuilder)
        {
            _remoteDatatableBuilder = remoteDatatableBuilder;
        }

        public IRemoteDatatableColumnBuilder<TEntity> Column(string header)
        {
            return _remoteDatatableBuilder.Column(header);
        }

        public IRemoteDatatableColumnBuilder<TEntity, bool?> Column(string header, Expression<Func<TEntity, bool?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, int?> Column(string header, Expression<Func<TEntity, int?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, double?> Column(string header, Expression<Func<TEntity, double?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, decimal?> Column(string header, Expression<Func<TEntity, decimal?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, long?> Column(string header, Expression<Func<TEntity, long?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, short?> Column(string header, Expression<Func<TEntity, short?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, string> Column(string header, Expression<Func<TEntity, string>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, DateTime?> Column(string header, Expression<Func<TEntity, DateTime?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, TimeSpan?> Column(string header, Expression<Func<TEntity, TimeSpan?>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public IRemoteDatatableColumnBuilder<TEntity, ICollection<TProperty>> Column<TProperty>(string header, Expression<Func<TEntity, ICollection<TProperty>>> propertyExpression)
        {
            return _remoteDatatableBuilder.Column(header, propertyExpression);
        }

        public RemoteDatatable<TEntity> Build()
        {
            return _remoteDatatableBuilder.Build();
        }

    }
}
