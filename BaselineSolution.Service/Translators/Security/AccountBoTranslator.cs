using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Security
{
    internal class AccountBoTranslator : ITranslator<AccountBo, Account>
    {
        public AccountBo FromModel(Account model)
        {
            var bo = new AccountBo();

            bo.Name = model.Name;
            bo.Description = model.Description;
            if(model.ParentId.HasValue)
                bo.Parent = new DisplayObject(model.Parent.Id, model.Parent.Name);

            return bo;
        }

        public Account UpdateModel(AccountBo bo, Account model)
        {
            model.Name = bo.Name;
            model.Description = bo.Description;
            model.ParentId = bo.Id;

            return model;
        }
    }
}
