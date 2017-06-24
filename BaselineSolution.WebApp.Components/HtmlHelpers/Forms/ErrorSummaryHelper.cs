using System;
using System.Linq;
using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Forms
{
    public static class ErrorSummaryHelper
    {
        /// <summary>
        ///     Generates a summary of all errors found in a form. This does almost exactly the same as Html.ValidationSummary()
        ///     but uses the twitter bootstrap html structure
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="title"></param>
        /// <param name="excludePropertyErrors"></param>
        /// <returns></returns>
        public static MvcHtmlString ErrorSummary<TModel>(this HtmlHelper<TModel> htmlHelper, string title,
            bool excludePropertyErrors = true)
        {
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");
            if (htmlHelper.ViewData.ModelState.IsValid)
                return null;

            var errorMessages = htmlHelper.GetModelStateList(excludePropertyErrors)
                .SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .Where(e => !string.IsNullOrWhiteSpace(e))
                .Distinct()
                .ToList();
            if (!errorMessages.Any())
                return null;


            var alertDiv = new TagBuilder("div")
                .Class("alert alert-danger");

            var closeButton = new TagBuilder("button")
                .Attribute("type", "button")
                .Attribute("data-dismiss", "alert")
                .Class("close")
                .Html("&times;");

            var titleStrong = new TagBuilder("strong")
                .Html(title);

            var errorUl = new TagBuilder("ul");

            foreach (string errorMessage in errorMessages)
            {
                errorUl.AppendHtml(new TagBuilder("li").Html(errorMessage));
            }

            alertDiv
                .AppendHtml(closeButton)
                .AppendHtml(titleStrong)
                .AppendHtml(errorUl);

            return alertDiv.ToHtml();
        }
    }
}
