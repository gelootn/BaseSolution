using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.DAL.UnitOfWork.Interfaces;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Internal
{
    public class CrudService<TBo, TCommitBo, TEntity> : ICrudService<TBo, TCommitBo>
        where TCommitBo : BaseBo
        where TBo : BaseBo
        where TEntity : Entity, new()
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly ICrudTranslator<TBo, TCommitBo, TEntity> _translator;
        private IUnitOfWork _unitOfWork;

        public CrudService(IGenericRepository<TEntity> repository, ICrudTranslator<TBo, TCommitBo, TEntity> translator)
        {
            _repository = repository;
            _translator = translator;
        }

        public Response<TBo> GetById(int id)
        {
            var item = _repository.FindById(id);
            if (item == null)
                return new Response<TBo>().AddItemNotFound(id);

            var bo = _translator.ToBo(item);

            return new Response<TBo>(bo);
        }

        public Response<int> AddOrUpdate(TCommitBo bo, int userId)
        {
            if (_unitOfWork == null)
                throw new UnitOfWorkException(BoResources.UnitOfWorkNotSet);

            if (!bo.IsValid())
                return new Response<int>().AddValidationMessage(bo.ValidationMessages);

            var item = _repository.FindById(bo.Id) ?? new TEntity();

            item = _translator.UpdateModel(bo, item);

            _repository.AddOrUpdate(item);
            _unitOfWork.Commit(userId);
            return new Response<int>(item.Id);
        }

        public Response<bool> Delete(TBo bo, int userId)
        {
            if (_unitOfWork == null)
                throw new UnitOfWorkException(BoResources.UnitOfWorkNotSet);

            var item = _repository.FindById(bo.Id);
            if (item == null)
                return new Response<bool>().AddItemNotFound(bo.Id);

            _repository.Delete(bo.Id);
            _unitOfWork.Commit(userId);
            return new Response<bool>(true);
        }

        public void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



    }
}
