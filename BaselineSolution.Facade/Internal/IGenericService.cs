using BaselineSolution.Bo.Internal;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Internal
{
    public interface IGenericService<TBo> where TBo : BaseBo
    {
        Response<TBo> GetById(int id);
        Response<TBo> AddOrUpdate(TBo bo);
        Response<bool> Delete(int id);
        Response<int> Count(IEntityFilter<TBo> filter);
        Response<TBo> List(IEntityFilter<TBo> filter, IEntitySorter<TBo> sorter, int page, int pageSize);
    }
}
