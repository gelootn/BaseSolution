using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Autofac.Features.ResolveAnything;
using BaselineSolution.Bo.Models.Security;
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
            var response = _securityMgntService.UserCrudService.GetById(id ?? 0);

            if (response.IsSuccess)
            {
                vm.User = response.GetValue();
            }
            else
            {
                vm.User = new UserBo();
            }

            

            return View(vm);
        }
    }
}