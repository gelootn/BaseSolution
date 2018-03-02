using System;
using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.Service.Translators.Internal
{
    internal abstract class Translator <TBo, TEntity> 
        where TBo : BaseBo 
        where TEntity : Entity
    {
        public abstract TBo FromModel(TEntity model);

        public virtual TEntity UpdateModel(TBo bo, TEntity model)
        {
            throw new ArgumentException(GetType() + " does not implement UpdateModel");
        }

    }
}
