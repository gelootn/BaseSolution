using System.Web.Mvc;
using BaselineSolution.Facade.Security;
using BaselineSolution.WebApp.Areas.Security.ViewModels.User;

namespace BaselineSolution.WebApp.Areas.Security.Controllers
{
    public class UserController : Controller
    {
        private readonly ISecurityMgntService _securityMgntService;

        public UserController(ISecurityMgntService securityMgntService)
        {
            _securityMgntService = securityMgntService;
        }

        // GET: Security/User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int? id)
        {
            var vm = new EditViewModel();

            

            return View(vm);
        }
    }
}