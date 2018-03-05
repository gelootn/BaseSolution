using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Forms
{
    public static class SubmitAndCancelHelper
    {
        public static MvcHtmlString SubmitAndCancel(this HtmlHelper html,
            string saveLabel,
            string cancelLabel,
            string cancelLink = "")
        {
            var submit = html.Submit(saveLabel);

            var cancel = html.Cancel(cancelLabel, cancelLink);

            var controls = new TagBuilder("div")
                .Class("form-actions")
                .AppendHtml(submit)
                .AppendHtml(cancel);

            return controls.ToHtml();
        }

        public static MvcHtmlString SubmitAndCancelFloat(this HtmlHelper html,
            string saveLabel,
            string cancelLabel,
            string cancelLink)
        {
            var ul = new TagBuilder("ul");

            var submit = html.SubmitLink(saveLabel);
            var cancel = html.CancelLink(cancelLabel, cancelLink);

            ul.AppendHtml(new TagBuilder("li").AppendHtml(cancel)).AppendHtml(new TagBuilder("li").AppendHtml(submit));

            return ul.ToHtml();
        }
    }
}
