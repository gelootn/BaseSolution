using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class RoleBoTranslator : Translator<RoleBo, Role>
    {
        public override RoleBo FromModel(Role model)
        {
            var bo = new RoleBo();
            bo.Name = model.Name;
            bo.ParentId = model.ParentId;
            if(model.ParentId.HasValue)
                bo.Parent = new DisplayObject(model.Parent.Id, model.Parent.Name);

            return bo;
        }

        public override Role UpdateModel(RoleBo bo, Role model)
        {
            model.Name = bo.Name;
            model.ParentId = bo.ParentId;

            return model;
        }
    }
}
