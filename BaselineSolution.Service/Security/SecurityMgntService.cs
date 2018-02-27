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

        public SecurityMgntService(ISecurityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork; 
        }


        public Response<UserBo> GetUserList()
        {
            var list = _unitOfWork.UserRepo.List().ToList();
            
            

            return new Response<UserBo>();
        }

        public Response<bool> SetUserPassword(UserSetPasswordBo bo)
        {
            if(!bo.IsValid())
                return new Response<bool>().AddValidationMessage(bo.ValidationMessages);

            return new Response<bool>();
        }
    }
}
