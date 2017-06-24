using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BaselineSolution.WebApp.Components.Extensions
{
    public static class ExtensionsForHtmlHelper
    {
        /*        public static void RenderPartial<TModel, TProperty>(this HtmlHelper<TModel> helper, [AspMvcView] string partialViewName, Expression<Func<TModel, TProperty>> expression)
                    where TProperty: class, new()
                {
                    var model = helper.ViewData.Model;
                    var childModel = expression.Compile()(model) ?? new TProperty();
                    helper.ViewData.TemplateInfo.HtmlFieldPrefix = ExpressionHelper.GetExpressionText(expression);
                    helper.RenderPartial(partialViewName, childModel, helper.ViewData);
                }*/

        public static IEnumerable<ModelState> GetModelStateList(this HtmlHelper htmlHelper, bool excludePropertyErrors)
        {
            if (excludePropertyErrors)
            {
                ModelState modelState;
                htmlHelper.ViewData.ModelState.TryGetValue(htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix, out modelState);
                if (modelState == null)
                    return new ModelState[0];
                return new ModelState[1]
                {
                    modelState
                };
            }
            var ordering = new Dictionary<string, int>();
            ModelMetadata modelMetadata1 = htmlHelper.ViewData.ModelMetadata;
            if (modelMetadata1 != null)
            {
                foreach (ModelMetadata modelMetadata2 in modelMetadata1.Properties)
                    ordering[modelMetadata2.PropertyName] = modelMetadata2.Order;
            }
            return
                htmlHelper.ViewData.ModelState.Select(kv => new
                {
                    kv = kv,
                    name = kv.Key
                }).OrderBy(keyValue => GetOrDefault(ordering, keyValue.name, 10000)).Select(param0 => param0.kv.Value);
        }

        private static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue @default)
        {
            TValue obj;
            if (dict.TryGetValue(key, out obj))
                return obj;
            else
                return @default;
        }
    }
}
