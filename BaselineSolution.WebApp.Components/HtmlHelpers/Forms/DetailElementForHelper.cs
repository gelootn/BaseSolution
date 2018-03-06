using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Forms
{
    public static class DetailElementForHelper
    {
        /// <summary>
        ///     Generates a formatted displayName and value using the default MVC DisplayFor and DisplayNameFor methods
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static MvcHtmlString DetailElementFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                         Expression<Func<TModel, TProperty>>
                                                                             propertyExpression)
        {
            return DetailElementFor(htmlHelper,
                                    htmlHelper.DisplayFor(propertyExpression).ToString(),
                                    htmlHelper.DisplayNameFor(propertyExpression).ToString());
        }

        /// <summary>
        ///     Generates a formatted displayname and value using the default MVC DisplayFor method and a custom display name
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="propertyExpression"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static MvcHtmlString DetailElementFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                         Expression<Func<TModel, TProperty>>
                                                                             propertyExpression,
                                                                         string displayName)
        {
            return DetailElementFor(htmlHelper,
                                    htmlHelper.DisplayFor(propertyExpression).ToString(),
                                    displayName);
        }

        /// <summary>
        ///     Generates a formatted custom displayname and value
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="value"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static MvcHtmlString DetailElementFor<TModel>(this HtmlHelper<TModel> htmlHelper,
                                                              string value,
                                                              string displayName)
        {
            return new TagBuilder("dl")
                .Class("dl-horizontal")
                .AppendHtml(new TagBuilder("dt").Html(displayName))
                .AppendHtml(new TagBuilder("dd").Html(value))
                .ToHtml();
        }
    }
}
