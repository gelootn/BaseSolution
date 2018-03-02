using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class RightBoTranslator : Translator<RightBo, Right>
    {
        public override RightBo FromModel(Right model)
        {
            var bo = new RightBo();

            bo.Key = model.Key;
            bo.ParentId = model.ParentId;
            if(model.ParentId.HasValue)
                bo.Parent = new DisplayObject(model.Parent.Id, model.Parent.Key);

            return bo;
        }

        public override Right UpdateModel(RightBo bo, Right model)
        {
            model.Key = bo.Key;
            model.ParentId = bo.ParentId;

            return model;
        }
    }
}
