using System;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Facade.Infrastructure;

namespace BaselineSolution.Service.Internal
{
    public abstract class GenericService<TBo> : IGenericService<TBo>
    {
        public GenericService(IGenericRepository<>)
        {
            
        }

        public Response<TBo> GetAll()
        {
            throw new NotImplementedException();
        }

        public Response<TBo> GetById()
        {
            throw new NotImplementedException();
        }

        public Response<int> AddOrUpdate(TBo bo)
        {
            throw new NotImplementedException();
        }

        public Response<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
