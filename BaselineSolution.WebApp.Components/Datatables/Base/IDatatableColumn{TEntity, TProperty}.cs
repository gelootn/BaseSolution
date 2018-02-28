using System;
using System.Linq.Expressions;

namespace BaselineSolution.WebApp.Components.Datatables.Base
{
    public interface IDatatableColumn<TEntity, TProperty>: IDatatableColumn<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, TProperty>> PropertyExpression { get; set; }

        TProperty GetProperty(TEntity entity);
    }
}
