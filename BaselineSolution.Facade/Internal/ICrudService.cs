using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.UnitOfWork.Interfaces;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Facade.Internal
{
    public interface ICrudService<TBo, TCommitBo> where TBo : BaseBo where TCommitBo : BaseBo
    {
        Response<TBo> GetById(int id);
        Response<int> AddOrUpdate(TCommitBo bo, int userId);
        Response<bool> Delete(TBo bo, int userId);

        void SetUnitOfWork(IUnitOfWork unitOfWork);
    }
}