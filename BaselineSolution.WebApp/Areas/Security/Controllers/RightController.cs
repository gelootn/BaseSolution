using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Facade.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.Framework.Infrastructure.Sorting;
using BaselineSolution.WebApp.Areas.Security.ViewModels.Right;
using BaselineSolution.WebApp.Infrastructure.Bases;
using BaselineSolution.WebApp.Infrastructure.Utilities;

namespace BaselineSolution.WebApp.Areas.Security.Controllers
{
    public class RightController : BaseController
    {
        private readonly ISecurityMgntService _service;


        public RightController(ISecurityMgntService service)
        {
            _service = service;
        }
        // GET: Security/Right
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetRights()
        {
            var response = _service.GetTopLevelRights();
            if (response.IsSuccess)
            {
                var rights = response.Values;
                return Json(rights.Select(r => r.ToJson()), JsonRequestBehavior.AllowGet);
            }

            return HttpNotFound(string.Join(", ", response.Messages.Select(x => x.MessageText)));
        }

        public JsonResult GenerateRights()
        {
            var generatedRights = ExploreToRights();
            var response = _service.GetTopLevelRights();

            if (!response.IsSuccess) 
                return null;

            var existingRights = response.Values;
            var mergedRights = MergeRights(existingRights, generatedRights);
            return Json(mergedRights.Select(r => r.ToJson()), JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveRights(RightBo[] rights)
        {
            foreach (var right in rights)
            {
                _service.RightService.AddOrUpdate(right,User.Id);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Edit(int? id)
        {
            var rightBo = new RightBo();
            if (id.HasValue)
            {
                var response = _service.RightService.GetById(id.Value);
                if (response.IsSuccess)
                {
                    rightBo = response.GetValue();
                }
            }

            return View(CreateEditViewModel(rightBo));

        }

        [HttpPost]
        public ActionResult Edit([Bind(Prefix = "RightBo")] RightBo rightBo)
        {
            if (!ModelState.IsValid)
                return View(CreateEditViewModel(rightBo));

            _service.RightService.AddOrUpdate(rightBo,User.Id);
            return RedirectToAction("Index");
        }

        private IEnumerable<RightBo> ExploreToRights()
        {
            var name = Assembly.GetExecutingAssembly().GetName().Name; // yo dawg
            var exploreResult = RouteUtil.Explore(name);
            var rights = exploreResult
                .Select(area =>
                    new RightBo
                    {
                        Key = area.Key,
                        Children = area.Value
                            .Where(c => !c.Key.HasAttribute(typeof(AllowAnonymousAttribute))
                                        && !c.Key.HasAttribute(typeof(IgnoreAuthenticationAttribute)))
                            .Select(controller =>
                                new RightBo
                                {
                                    Key = string.Join(".", area.Key, controller.Key.Name),
                                    Children = controller.Value
                                        .GroupBy(c => c.Name)
                                        .Select(c => c.First())
                                        .Where(method => !method.HasAttribute(typeof(AllowAnonymousAttribute))
                                                         && !method.HasAttribute(typeof(IgnoreAuthenticationAttribute)))
                                        .Select(action =>
                                            new RightBo
                                            {
                                                Key = string.Join(".", area.Key, controller.Key.Name, action.Name)
                                            })
                                        .ToList()
                                })
                            .ToList()
                    }
                );
            return rights;
        }

        private IEnumerable<RightBo> MergeRights(IEnumerable<RightBo> existingRights,
            IEnumerable<RightBo> generatedRights)
        {
            var rights = existingRights.ToList();
            foreach (var generatedRight in generatedRights)
            {
                var existingRight = rights.SingleOrDefault(r => string.Equals(r.Key, generatedRight.Key));
                if (existingRight != null)
                {
                    existingRight.MergeWith(generatedRight);
                }
                else
                {
                    rights.Add(generatedRight);
                }
            }
            return rights;
        }

        private EditViewModel CreateEditViewModel(RightBo rightBo)
        {
            var vm = new EditViewModel();
            vm.RightBo = rightBo;

            var response = _service.RightService.List(EntityFilter<RightBo>.Where(x => x.Id != rightBo.Id));

            if (response.IsSuccess)
                vm.Rights = response.Values.ToSelectList(x=> x.Id, x => x.Key);


            return vm;

        }
    }
}