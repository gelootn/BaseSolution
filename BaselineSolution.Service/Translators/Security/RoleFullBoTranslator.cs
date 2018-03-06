using System.Linq;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    public class RoleFullBoTranslator : ITranslator<RoleFullBo, Role>
    {
        public RoleFullBo FromModel(Role model)
        {
            var bo = new RoleFullBo();

            bo.Id = model.Id;
            bo.Name = model.Name;
            bo.RoleRights = model.RoleRights.Where(x => !x.Deleted).Select(x => x.ToBo(new RoleRightBoTranslator())).ToList();

            return bo;
        }

        public Role UpdateModel(RoleFullBo bo, Role model)
        {
            model.RoleRights.UpdateWith(bo.RoleRights, new RoleRightBoTranslator());

            return model;
        }
    }
}
