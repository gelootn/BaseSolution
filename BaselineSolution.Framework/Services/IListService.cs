using System.Collections.Generic;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.Framework.Services
{
    public interface IListService<TEntity>  where TEntity : class, IIdentifiable
    {
        int Count(IEntityFilter<TEntity> baseFilter);
        IEnumerable<TEntity> List(IEntitySorter<TEntity> sorter, IEntityFilter<TEntity> filter, int page, int pageSize);
    }
}
