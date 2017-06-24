using System.Web.Mvc;
using BaselineSolution.Bo.Internal.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.WebApp.Areas.Home.ViewModels.Authentication;

namespace BaselineSolution.WebApp.Areas.Home.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ISecurityService _securityService;

        public AuthenticationController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        // GET: Home/Authentication
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            UserSecurityBo bo;
            var response = _securityService.Login(model.UserName, model.Password, out bo);
            if (response.IsSuccess)
            {
                
            }
            
                

            



            return View();
        }
    }
}