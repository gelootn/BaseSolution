using System.Linq;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class RightBoTranslator : ITranslator<RightBo, Right>
    {
        public  RightBo FromModel(Right model)
        {
            var bo = new RightBo();

            bo.Id = model.Id;
            bo.Key = model.Key;
            bo.ParentId = model.ParentId;
            if (model.ParentId.HasValue && model.Parent != null) 
                bo.Parent = new DisplayObject(model.Parent.Id, model.Parent.Key);

            if(model.Children != null && model.Children.Any())
            {
                bo.Children = model.Children.Select(x => x.ToBo(this)).ToList();
            }
            return bo;
        }

        public  Right UpdateModel(RightBo bo, Right model)
        {
            model.Key = bo.Key;
            model.ParentId = bo.ParentId;

            model.Children.UpdateWith(bo.Children, this);

            return model;
        }
    }
}
