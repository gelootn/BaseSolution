using System.Linq;
using BaselineSolution.Bo.Filters.Security;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Translators.Security;

namespace BaselineSolution.Service.Security
{
    public class SecurityMgntService : ISecurityMgntService
    {
        private readonly ISecurityUnitOfWork _unitOfWork;

        public SecurityMgntService(ISecurityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public Response<UserBo> GetUserList(UserFilter filter)
        {
            var list = _unitOfWork.UserRepo.List();
            if (!string.IsNullOrWhiteSpace(filter.Email))
                list = list.Where(x => x.Email.Contains(filter.Email));
            if (!string.IsNullOrWhiteSpace(filter.UserName))
                list = list.Where(x => x.Username.Contains(filter.UserName));

            var users = list.Select(x => x.ToUserBo()).ToList();

            return new Response<UserBo>(users);
        }

        public Response<bool> SetUserPassword(UserSetPasswordBo bo)
        {
            if (!bo.IsValid())
                return new Response<bool>().AddValidationMessage(bo.ValidationMessages);

            return new Response<bool>();
        }

    }
}
