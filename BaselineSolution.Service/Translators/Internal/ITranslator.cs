using System;
using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.Service.Translators.Internal
{
    public interface ITranslator <TBo, TEntity> 
        where TBo : BaseBo 
        where TEntity : Entity
    {
        TBo FromModel(TEntity model);
        TEntity UpdateModel(TBo bo, TEntity model);
    }
}
