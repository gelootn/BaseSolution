using System;
using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Response;

namespace BaselineSolution.Service.Internal
{
    public class GenericService<TBo, TEntity> : IGenericService<TBo> 
        where TBo : BaseBo
        where TEntity : Entity
    {
        private readonly IGenericRepository<TEntity> _repository;

        public GenericService(IGenericRepository<TEntity> repository)
        {
            _repository = repository;
        }

        Response<TBo> IGenericService<TBo>.GetById(int id)
        {


            throw new NotImplementedException();
        }

        Response<TBo> IGenericService<TBo>.AddOrUpdate(TBo bo)
        {
            throw new NotImplementedException();
        }

        Response<bool> IGenericService<TBo>.Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
