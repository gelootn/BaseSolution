using System.Linq;
using System.Web.Mvc;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.WebApp.Areas.Security.ViewModels.Account;
using BaselineSolution.WebApp.Components.Datatables.Remote;
using BaselineSolution.WebApp.Components.Datatables.Remote.Processors;
using BaselineSolution.WebApp.Components.Datatables.Remote.Reply;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;
using BaselineSolution.WebApp.Components.Utilities;
using BaselineSolution.WebApp.Infrastructure.Bases;

namespace BaselineSolution.WebApp.Areas.Security.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ISecurityMgntService _service;

        public AccountController(ISecurityMgntService service)
        {
            _service = service;
        }

        // GET: Security/Account
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult AccountList(DatatableRequest request)
        {
            var childAccounts = User.MainAccount.Flatten();
            var allowedAccounts = childAccounts.Select(x => x.Id);

            var datatable = DatatableStorage.Get<AccountBo>(request.DatatableId, () => RazorViews.RenderToString(this, "_AccountList"));
            var baseFilter = EntityFilter<AccountBo>.Where(x => allowedAccounts.Any(a => x.Id == a));

            var processor = new ServiceDatatableProcessor<AccountBo>(_service.AccountService, baseFilter);
            var replier = new DatatableReplier<AccountBo>(datatable, processor);
            return replier.Reply(request).ToJson();
        }

        public ActionResult Edit(int? id)
        {
            var account = new AccountBo();
            if (id.HasValue)
            {
                var response = _service.AccountService.GetById(id.Value);
                if (response.IsSuccess)
                    account = response.GetValue();
            }

            var vm = new EditViewModel();
            vm.AccountBo = account;

            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(AccountBo accountbo)
        {
            var vm = new EditViewModel();
            vm.AccountBo = accountbo;

            if (!ModelState.IsValid)
                return View(vm);


            var response = _service.AccountService.AddOrUpdate(accountbo);
            if (!response.IsSuccess)
                return View(vm);


            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var response = _service.AccountService.GetById(id);
            if (response.IsSuccess)
            {

                return PartialView("_delete");
            }

            return HttpNotFound();

        }
    }
}