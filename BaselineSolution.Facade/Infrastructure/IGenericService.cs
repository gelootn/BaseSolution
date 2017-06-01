using BaselineSolution.Bo.Internal;

namespace BaselineSolution.Facade.Infrastructure
{
    public interface IGenericService<TBo>
    {
        Response<TBo> GetAll();
        Response<TBo> GetById();
        Response<int> AddOrUpdate(TBo bo);
        Response<bool> Delete(int id);

    }
}