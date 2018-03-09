using System.Web.Mvc;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.Facade.Security;
using BaselineSolution.Facade.Shared;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.WebApp.Areas.Security.ViewModels.User;
using BaselineSolution.WebApp.Components.Datatables.Remote;
using BaselineSolution.WebApp.Components.Datatables.Remote.Processors;
using BaselineSolution.WebApp.Components.Datatables.Remote.Reply;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;
using BaselineSolution.WebApp.Components.Utilities;
using BaselineSolution.WebApp.Infrastructure.Bases;

namespace BaselineSolution.WebApp.Areas.Security.Controllers
{
    public class UserController : BaseController
    {
        private readonly ISecurityMgntService _service;
        private readonly ISharedService _sharedService;

        public UserController(ISecurityMgntService securityMgntService, ISharedService sharedService)
        {
            _service = securityMgntService;
            _sharedService = sharedService;
        }

        // GET: Security/User
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(DatatableRequest request)
        {
            var datatable = DatatableStorage.Get<UserBo>(request.DatatableId, () => RazorViews.RenderToString(this, "_List"));
            var baseFilter = EntityFilter<UserBo>.AsQueryable();
            if (!User.IsAdministrator)
                baseFilter = baseFilter.Where(x => x.AccountId == User.MainAccount.Id);

            var processor = new ServiceDatatableProcessor<UserBo>(_service.UserService, baseFilter);
            var replier = new DatatableReplier<UserBo>(datatable, processor);
            return replier.Reply(request).ToJson();
        }

        public ActionResult Edit(int? id)
        {
            var bo = new UserBo();
            if (id.HasValue)
            {
                var response = _service.UserService.GetById(id.Value);
                if (response.IsSuccess)
                    bo = response.GetValue();
            }
            return View(CreateEditViewModel(bo));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "User")]UserBo bo)
        {
            if (!bo.IsValid())
                bo.ValidationMessages.ForEach(x => ModelState.AddModelError("", x));

            var existingUserResponse = _service.IsUsernameTaken(bo.Name, bo.Id);
            if (existingUserResponse.IsSuccess && !existingUserResponse.GetValue())
                ModelState.AddModelError("Username", Resources.SecurityResource.UserExists);

            if (!ModelState.IsValid)
                return View(CreateEditViewModel(bo));

            var response = _service.SaveUser(bo, User.Id);
            SetMessage(response);
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            var response = _service.UserService.GetById(id);
            if (response.IsSuccess)
            {
                var vm = new DeleteViewModel();
                vm.Bo = response.GetValue();
                return PartialView("_Delete", vm);
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (id == User.Id)
            {
                Error(Resources.SecurityResource.CannotDeleteSelf);
                return RedirectToAction("Index", "User");
            }


            var response = _service.UserService.Delete(id, User.Id);
            SetMessage(response);
            return RedirectToAction("Index", "User");
        }

        public ActionResult ResetPassword(int id)
        {
            var userResponse = _service.UserService.GetById(id);

            if (userResponse.IsSuccess)
            {
                var user = userResponse.GetValue();
                var vm = new ResetPasswordViewModel(user);
                return View(vm);
            }

            SetMessage(userResponse);
            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        public ActionResult ResetPassword([Bind(Prefix = "Password")] UserSetPasswordBo passwordBo)
        {
            if (!ModelState.IsValid)
            {
                var userResponse = _service.UserService.GetById(passwordBo.Id);
                return View(new ResetPasswordViewModel(userResponse.GetValue()));
            }

            var response =_service.ResetUserPassword(passwordBo, User.Id);
            SetMessage(response);
            return RedirectToAction("Index", "User");
        }


        private EditViewModel CreateEditViewModel(UserBo bo)
        {
            var vm = new EditViewModel();
            vm.User = bo;

            var roleRespone = _service.GetAllowedRoles(User.Id);
            if (roleRespone.IsSuccess)
                vm.Roles = roleRespone.Values.ToMultiSelectList(x => x.Id, x => x.Name);
            var accountResponse = _service.AccountService.List(EntityFilter<AccountBo>.AsQueryable());
            if (accountResponse.IsSuccess)
                vm.Accounts = accountResponse.Values;
            var languageResponse = _sharedService.SystemLanguageService.List(EntityFilter<SystemLanguageBo>.AsQueryable());
            if (languageResponse.IsSuccess)
                vm.Languages = languageResponse.Values;

            return vm;

        }

    }
}