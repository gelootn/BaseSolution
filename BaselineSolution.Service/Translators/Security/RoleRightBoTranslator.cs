using System;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class RoleRightBoTranslator : ITranslator<RoleRightBo, RoleRight>
    {
        public  RoleRightBo FromModel(RoleRight model)
        {
            var bo = new RoleRightBo();
            bo.Id = model.Id;
            bo.RoleId = model.RoleId;
            bo.RightId = model.RightId;
            bo.Allow = model.Allow;
            //bo.Role = new DisplayObject(model.RoleId, model.Role.Name);
            //bo.Right = new DisplayObject(model.RightId, model.Right.Key);
            return bo;
        }

        public  RoleRight UpdateModel(RoleRightBo bo, RoleRight model)
        {
            model.RoleId = bo.RoleId;
            model.RightId = bo.RightId;
            model.Allow = bo.Allow;
            return model;
        }
    }
}
