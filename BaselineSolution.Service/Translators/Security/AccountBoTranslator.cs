using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class AccountBoTranslator : ITranslator<AccountBo, Account>
    {
        public AccountBo FromModel(Account model)
        {
            var bo = new AccountBo();

            bo.Id = model.Id;
            bo.Name = model.Name;
            bo.Description = model.Description;
            bo.ParentId = model.ParentId;
            if (model.ParentId.HasValue)
                bo.Parent = new DisplayObject(model.Parent.Id, model.Parent.Name);

            if (model.Children != null && model.Children.Any())
            {
                var children = model.Children.Where(x=> !x.Deleted).Select(x => x.Flatten().Distinct()).SelectMany(x => x.Select(y => new DisplayObject(y.Id, y.Name)));
                bo.Children = children.ToList();
            }
            else
            {
                bo.Children = new List<DisplayObject>();
            }


            return bo;
        }

        public Account UpdateModel(AccountBo bo, Account model)
        {
            model.Name = bo.Name;
            model.Description = bo.Description;
            model.ParentId = bo.ParentId;

            return model;
        }
    }
}
