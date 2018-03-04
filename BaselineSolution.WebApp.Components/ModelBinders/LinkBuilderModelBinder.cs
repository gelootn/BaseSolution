using System;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Extensions;
using BaselineSolution.WebApp.Components.Models.Links;

namespace BaselineSolution.WebApp.Components.ModelBinders
{
    public class LinkBuilderModelBinder: DefaultModelBinder
    {
        /// <summary>
        /// Binds the model by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// The bound object.
        /// </returns>
        /// <param name="controllerContext">The context within which the controller operates. The context information includes the controller, HTTP content, request context, and route data.</param><param name="bindingContext">The context within which the model is bound. The context includes information such as the model object, model name, model type, property filter, and value provider.</param><exception cref="T:System.ArgumentNullException">The <paramref name="bindingContext "/>parameter is null.</exception>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext");
            }
            var areaName = bindingContext.GetValue<string>("AreaName");
            var controllerName = bindingContext.GetValue<string>("ControllerName");
            var actionName = bindingContext.GetValue<string>("ActionName");
            var isAllowed = bindingContext.GetValue<bool>("IsAllowed");
            var link = bindingContext.GetValue<string>("Link");
            var absoluteUrl = bindingContext.GetValue<string>("AbsoluteUrl");
            return new LinkBuilder(link, areaName, controllerName, actionName, isAllowed, absoluteUrl);
        }
    }
}
