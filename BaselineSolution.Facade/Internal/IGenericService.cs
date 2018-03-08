using BaselineSolution.Bo.Internal;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Internal
{
    public interface IGenericService<TBo>
            where TBo : BaseBo
    {
        Response<TBo> GetById(int id);
        /// <summary>
        /// Add or Update. returns the Id of the processed item
        /// </summary>
        /// <param name="bo"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Response<int> AddOrUpdate(TBo bo, int userId);
        Response<bool> Delete(int id, int userId);
        Response<int> Count(IEntityFilter<TBo> filter);
        Response<TBo> List(IEntityFilter<TBo> filter);
        Response<TBo> List(IEntityFilter<TBo> filter, IEntitySorter<TBo> sorter, int? page, int? pageSize);
    }
}
