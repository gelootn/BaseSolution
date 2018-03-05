using System.Web.Mvc;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Forms
{
    public static class CancelHelper
    {
        public static MvcHtmlString Cancel(this HtmlHelper html, string cancelLabel)
        {
            return Cancel(html, cancelLabel, string.Empty, null);
        }

        public static MvcHtmlString Cancel(this HtmlHelper html, string cancelLabel, object htmlAttributes)
        {
            return Cancel(html, cancelLabel, string.Empty, htmlAttributes);
        }

        public static MvcHtmlString Cancel(this HtmlHelper html, string cancelLabel, string cancelLink)
        {
            return Cancel(html, cancelLabel, cancelLink, null);
        }

        public static MvcHtmlString Cancel(this HtmlHelper html, string cancelLabel, string cancelLink, object htmlAttributes)
        {
            var cancel = new TagBuilder("a")
                .Attribute("href", string.IsNullOrWhiteSpace(cancelLink) ? "#" : cancelLink)
                .Class("enable-cancel")
                .Class("btn")
                .Html(cancelLabel)
                .Merge(htmlAttributes);

            return cancel.ToHtml();
        }

        public static MvcHtmlString CancelLink(this HtmlHelper html, string cancelLabel, object htmlAttributes)
        {
            return CancelLink(html, cancelLabel, "", htmlAttributes);
        }

        public static MvcHtmlString CancelLink(this HtmlHelper html, string cancelLabel, string cancelLink = null)
        {
            return CancelLink(html, cancelLabel, cancelLink, null);
        }

        public static MvcHtmlString CancelLink(this HtmlHelper html, string cancelLabel, string cancelLink, object htmlAttributes)
        {
            var cancel = new TagBuilder("a");
            cancel.Attributes.Add("title", cancelLabel);
            cancel.Attributes.Add("href", cancelLink);
            cancel.Class("tooltip-left")
                .Class("check-changes")
                .Merge(htmlAttributes)
                .AppendHtml(new TagBuilder("i").Class("fa fa-reply"));

            return cancel.ToHtml();
        }
    }
}
