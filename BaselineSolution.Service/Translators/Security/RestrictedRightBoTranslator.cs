using System.Linq;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.Service.Translators.Security
{
    public class RestrictedRightBoTranslator 
    {
        public RestrictedRightBo FromModel(Right model, User user)
        {
            var bo = new RestrictedRightBo();

            bo.Id = model.Id;
            bo.Key = model.Key;
            bo.ParentId = model.ParentId;

            bo.IsRestricted = user.HasRight(model);

            bo.Children = model.Children.Select(x => FromModel(x, user)).ToList();

            return bo;
        }
    }
}
