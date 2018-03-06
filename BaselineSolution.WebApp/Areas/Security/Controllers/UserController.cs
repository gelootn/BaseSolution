using System.Web.Mvc;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Security;
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

        public UserController(ISecurityMgntService securityMgntService)
        {
            _service = securityMgntService;
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


        private EditViewModel CreateEditViewModel(UserBo bo)
        {
            var vm = new EditViewModel();
            vm.User = bo;


            return vm;

        }
    }
}