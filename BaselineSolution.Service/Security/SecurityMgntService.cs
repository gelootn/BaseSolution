using System.Linq;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Translators.Security;

namespace BaselineSolution.Service.Security
{
    public class SecurityMgntService : ISecurityMgntService
    {
        private readonly ISecurityUnitOfWork _unitOfWork;

        public SecurityMgntService(ISecurityUnitOfWork unitOfWork, ICrudService<UserBo, UserCommitBo> userCrudService)
        {
            _unitOfWork = unitOfWork;

            UserCrudService = userCrudService;
            userCrudService.SetUnitOfWork(_unitOfWork);
            
        }

        public ICrudService<UserBo, UserCommitBo> UserCrudService { get; }

        public Response<UserBo> GetUserList()
        {
            var list = _unitOfWork.UserRepo.List().ToList();
            var translator = new UserCrudTranslator();
            var bolist = list.Select(x => translator.ToBo(x)).ToList();

            return new Response<UserBo>(bolist);
        }

        public Response<bool> SetUserPassword(UserSetPasswordBo bo)
        {
            if(!bo.IsValid())
                return new Response<bool>().AddValidationMessage(bo.ValidationMessages);




        }
    }
}
