using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Bo.Internal;
using BaselineSolution.DAL.Infrastructure.Bases;

namespace BaselineSolution.Service.Translators.Internal
{
    public interface ICrudTranslator<TBo, TCommitBo, TEntity>
        where TBo : BaseBo
        where TCommitBo : BaseBo
        where TEntity : Entity
    {
        TBo ToBo(TEntity model);
        TEntity UpdateModel(TCommitBo bo, TEntity model);
    }
}
