using System.IO;
using System.Web.Mvc;
using BaselineSolution.Framework.Infrastructure.Attributes;

namespace BaselineSolution.WebApp.Components.Utilities
{
    public static class RazorViews
    {
        /// <summary>
        /// Renders a view to a string
        /// </summary>
        /// <param name="controller">A controller that has access to the <paramref name="viewName"/></param>
        /// <param name="viewName">The name of the view to render</param>
        /// <param name="viewModel">The model to use</param>
        /// <returns>The rendered view as a string</returns>
        public static string RenderToString([NotNull] ControllerBase controller, [AspMvcView] [NotNull] string viewName,
            [CanBeNull] object viewModel = null)
        {
            controller.ViewData.Model = viewModel;
            var controllerContext = controller.ControllerContext;
            using (var stringWriter = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var viewContext = new ViewContext(controllerContext, viewResult.View, controller.ViewData,
                    controller.TempData, stringWriter);
                viewResult.View.Render(viewContext, stringWriter);
                viewResult.ViewEngine.ReleaseView(controllerContext, viewResult.View);
                return stringWriter.GetStringBuilder().ToString();
            }
        }
    }
}
