using System.Linq;
using System.Web.Mvc;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.WebApp.Areas.Security.ViewModels.Role;
using BaselineSolution.WebApp.Components.Datatables.Remote;
using BaselineSolution.WebApp.Components.Datatables.Remote.Processors;
using BaselineSolution.WebApp.Components.Datatables.Remote.Reply;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;
using BaselineSolution.WebApp.Components.Utilities;
using BaselineSolution.WebApp.Infrastructure.Bases;

namespace BaselineSolution.WebApp.Areas.Security.Controllers
{
    public class RoleController : BaseController
    {
        private readonly ISecurityMgntService _service;

        public RoleController(ISecurityMgntService service)
        {
            _service = service;
        }

        // GET: Security/Role
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(DatatableRequest request)
        {
            var datatable = DatatableStorage.Get<RoleBo>(request.DatatableId, () => RazorViews.RenderToString(this, "_List"));
            var baseFilter = EntityFilter<RoleBo>.AsQueryable();

            var processor = new ServiceDatatableProcessor<RoleBo>(_service.RoleService, baseFilter);
            var replier = new DatatableReplier<RoleBo>(datatable, processor);
            return replier.Reply(request).ToJson();
        }

        public ActionResult Edit(int? id)
        {
            var bo = new RoleBo();

            if (id.HasValue)
            {
                var response = _service.RoleService.GetById(id.Value);
                if (response.IsSuccess)
                    bo = response.Value;
            }
            return View(CreateEditViewModel(bo));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "RoleBo")]RoleBo role)
        {
            if (!ModelState.IsValid)
                return View(CreateEditViewModel(role));

            var response = _service.RoleService.AddOrUpdate(role, User.Id);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var response = _service.RoleService.GetById(id);
            if (response.IsSuccess)
            {
                var vm = new DeleteViewModel();
                vm.RoleBo = response.Value;
                return PartialView("_Delete", vm);
            }

            return HttpNotFound();

        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            _service.RoleService.Delete(id, User.Id);

            return RedirectToAction("Index");
        }


        public ActionResult Rights(int id)
        {
            if (User.RoleIds.Contains(id) && !User.IsAdministrator)
            {
                return RedirectToAction("Index");
            }
            var user = _service.UserService.GetById(User.Id);
            var role = _service.GetFullRole(id);
            var allowedRights = _service.GetRestrictedRights(User.Id);
            var vm = new RoleRightsViewModel();

            vm.RoleBo = role.Value;
            vm.User = user.Value;
            vm.AllowedRights = allowedRights.Values;

            return View(vm);
        }

        [HttpPost]
        public ActionResult SaveRoleRight(int roleId, int rightId, bool? allow)
        {
            var roleResponse = _service.GetFullRole(roleId);
            if (roleResponse.IsSuccess)
            {
                var role = roleResponse.Value;
                var roleRight = role.RoleRights.SingleOrDefault(x => x.RightId == rightId);
                if (roleRight != null)
                {
                    roleRight.Allow = allow;
                }
                else
                {
                    role.RoleRights.Add(new RoleRightBo { RightId = rightId, Allow = allow });
                }

                _service.SaveFullRole(role, User.Id);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        private EditViewModel CreateEditViewModel(RoleBo bo)
        {
            var vm = new EditViewModel();
            vm.RoleBo = bo;
            var roleResponse = _service.GetAllowedRoles(User.Id);
            if(roleResponse.IsSuccess)
                vm.AllowedRoles = roleResponse.Values;
            return vm;
        }



    }
}