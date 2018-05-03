using System;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.Facade.Shared;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.WebApp.Areas.Setup.ViewModels.SystemLanguage;
using BaselineSolution.WebApp.Components.Datatables.Remote;
using BaselineSolution.WebApp.Components.Datatables.Remote.Processors;
using BaselineSolution.WebApp.Components.Datatables.Remote.Reply;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;
using BaselineSolution.WebApp.Components.Utilities;
using BaselineSolution.WebApp.Infrastructure.Bases;

namespace BaselineSolution.WebApp.Areas.Setup.Controllers
{
    public class SystemLanguageController : BaseController
    {
        private readonly ISharedService _service;

        public SystemLanguageController(ISharedService service)
        {
            _service = service;
        }

        // GET: Setup/SystemLanguage
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List(DatatableRequest request)
        {
            var datatable = DatatableStorage.Get<SystemLanguageBo>(request.DatatableId, () => RazorViews.RenderToString(this, "_List"));
            var baseFilter = EntityFilter<SystemLanguageBo>.AsQueryable();

            var processor = new ServiceDatatableProcessor<SystemLanguageBo>(_service.SystemLanguageService, baseFilter);
            var replier = new DatatableReplier<SystemLanguageBo>(datatable, processor);
            return replier.Reply(request).ToJson();
        }

        public ActionResult Edit(int? id)
        {
            var bo = new SystemLanguageBo();
            if (id.HasValue)
            {
                var response = _service.SystemLanguageService.GetById(id.Value);
                if (response.IsSuccess)
                    bo = response.Value;
                else
                    SetMessage(response);
            }

            return View(CreateEditViewModel(bo));
        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "SystemLanguage")]SystemLanguageBo bo)
        {
            if (!bo.IsValid())
                ModelState.AddModelError("", string.Join(", ", bo.ValidationMessages));
            if (!ModelState.IsValid)
                return View(CreateEditViewModel(bo));

            var response =_service.SystemLanguageService.AddOrUpdate(bo, User.Id);
            SetMessage(response);
            return RedirectToAction("Index");

        }

        public ActionResult Delete(int id)
        {
            var response = _service.SystemLanguageService.GetById(id);
            if (response.IsSuccess)
            {
                var vm = new DeleteViewModel();
                vm.SystemLanguage = response.Value;
                return PartialView("_Delete", vm );
            }

            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var response = _service.SystemLanguageService.Delete(id, User.Id);
            SetMessage(response);
            return RedirectToAction("Index");
        }


        public ActionResult Cultures(string id, string term, int page, int pageSize)
        {
            Func<CultureInfo, object> cultureToJson = c => new
            {
                id = c.Name,
                text = string.Format("{0} - {1}", c.Name, c.EnglishName)
            };
            if (id != null)
            {
                var culture = CultureInfo.GetCultureInfo(id);
                return Json(cultureToJson(culture), JsonRequestBehavior.AllowGet);
            }

            var availableCultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures | CultureTypes.NeutralCultures);
            Func<CultureInfo, bool> predicate = c => true;
            if (!string.IsNullOrEmpty(term))
            {
                predicate =
                    c =>
                        c.Name.ToLower().Contains(term.ToLower()) ||
                        c.EnglishName.ToLower().Contains(term.ToLower());
            }
            var cultures = availableCultures
                .Where(predicate)
                .OrderBy(c => c.Name)
                .Skip(page * pageSize)
                .Take(pageSize)
                .Select(cultureToJson).ToList();

            return Json(new { results = cultures, more = cultures.Count() == pageSize }, JsonRequestBehavior.AllowGet);

        }

        private EditViewModel CreateEditViewModel(SystemLanguageBo bo)
        {
            var vm = new EditViewModel();
            vm.SystemLanguage = bo;
            return vm;
        }


    }
}