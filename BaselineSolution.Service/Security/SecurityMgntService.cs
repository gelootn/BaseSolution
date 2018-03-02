using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Response;
using BaselineSolution.Framework.Security;
using BaselineSolution.Framework.Services;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Translators.Security;

namespace BaselineSolution.Service.Security
{
    public class SecurityMgntService : ISecurityMgntService
    {

        private readonly ISecurityUnitOfWork _unitOfWork;

        public SecurityMgntService(ISecurityUnitOfWork unitOfWork, IListService<UserBo> userListService, IListService<AccountBo> accountListService)
        {
            _unitOfWork = unitOfWork;
            UserListService = userListService;
            AccountListService = accountListService;
        }

        public IListService<UserBo> UserListService { get; }

        Response<UserBo> ISecurityMgntService.GetUserById(int id)
        {
            var user = _unitOfWork.UserRepo.FindById(id);
            if(user == null)
                return new Response<UserBo>().AddItemNotFound(id);

            return new Response<UserBo>(user.ToBo(new UserBoTranslator()));
        }

        Response<UserBo> ISecurityMgntService.AddOrUpdateUser(UserBo bo)
        {
            if(!bo.IsValid())
                return new Response<UserBo>().AddValidationMessage(bo.ValidationMessages);

            var user = bo.IsNew ? new User() : _unitOfWork.UserRepo.FindById(bo.Id);

            if(user == null)
                return new Response<UserBo>().AddItemNotFound(bo.Id);

            user = bo.UpdateModel(user, new UserBoTranslator());
            _unitOfWork.UserRepo.AddOrUpdate(user);
            _unitOfWork.Commit(0);

            return new Response<UserBo>(user.ToBo(new UserBoTranslator()));

        }

        Response<bool> ISecurityMgntService.DeleteUser(int id)
        {
            var user = _unitOfWork.UserRepo.FindById(id);
            if(user == null)
                return new Response<bool>().AddItemNotFound(id);

            _unitOfWork.UserRepo.Delete(id);
            _unitOfWork.Commit(0);

            return new Response<bool>(true);
        }

        Response<bool> ISecurityMgntService.SetUserPassword(UserSetPasswordBo bo)
        {
            if (!bo.IsValid())
                return new Response<bool>().AddValidationMessage(bo.ValidationMessages);

            var user = _unitOfWork.UserRepo.FindById(bo.Id);
            if(user == null)
                return new Response<bool>().AddItemNotFound(bo.Id);

            user.Password = PasswordHasher.CreateHash(bo.NewPassword);
            _unitOfWork.UserRepo.AddOrUpdate(user);
            _unitOfWork.Commit(0);

            return new Response<bool>();
        }

        public IListService<AccountBo> AccountListService { get; }

        Response<AccountBo> ISecurityMgntService.GetAccountById(int id)
        {
            var account = _unitOfWork.AccountRepo.FindById(id);
            if(account == null)
                return new Response<AccountBo>().AddItemNotFound(id);
            return new Response<AccountBo>(account.ToBo(new AccountBoTranslator()));
        }

        Response<AccountBo> ISecurityMgntService.AddOrUpdateAccount(AccountBo bo)
        {
            if(!bo.IsValid())
                return new Response<AccountBo>().AddValidationMessage(bo.ValidationMessages);

            var account = bo.IsNew ? new Account() : _unitOfWork.AccountRepo.FindById(bo.Id);

            account = bo.UpdateModel(account, new AccountBoTranslator());
            _unitOfWork.AccountRepo.AddOrUpdate(account);
            _unitOfWork.Commit(0);

            return new Response<AccountBo>(account.ToBo(new AccountBoTranslator()));
        }

        Response<bool> ISecurityMgntService.DeleteAccount(int id)
        {
            var account = _unitOfWork.AccountRepo.FindById(id);
            if(account == null)
                return new Response<bool>().AddItemNotFound(id);

            _unitOfWork.AccountRepo.Delete(id);
            _unitOfWork.Commit(0);

            return new Response<bool>(true);
        }
    }
}
