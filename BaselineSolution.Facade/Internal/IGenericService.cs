using BaselineSolution.Bo.Internal;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Internal
{
    public interface IGenericService<TBo> where TBo : BaseBo
    {
        Response<TBo> GetById(int id);
        Response<TBo> AddOrUpdate(TBo bo);
        Response<bool> Delete(int id);
    }
}
