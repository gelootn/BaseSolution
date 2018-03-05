using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Forms
{
    public static class SubmitHelper
    {
        public static MvcHtmlString Submit([NotNull] this HtmlHelper html, [NotNull] string value)
        {
            return Submit(html, value, "btn-primary", null);
        }

        public static MvcHtmlString Submit([NotNull] this HtmlHelper html, [NotNull] string value, string @class)
        {
            return Submit(html, value, @class, null);
        }

        public static MvcHtmlString Submit([NotNull] this HtmlHelper html, [NotNull] string value, [CanBeNull] object htmlAttributes)
        {
            return Submit(html, value, "btn-primary", htmlAttributes);
        }

        public static MvcHtmlString Submit([NotNull] this HtmlHelper html, [NotNull] string value, string @class, [CanBeNull] object htmlAttributes)
        {
            return new TagBuilder("input").Merge(htmlAttributes)
                .Attribute("type", "submit")
                .Attribute("value", value)
                .Class("btn")
                .Class(@class)
                .ToHtml();
        }


        public static MvcHtmlString SubmitLink([NotNull] this HtmlHelper html, string saveLabel)
        {
            var submit = new TagBuilder("a");
            submit.Attributes.Add("id", "save-button");
            submit.Attributes.Add("title", saveLabel);
            submit.Class("tooltip-left");
            submit.AppendHtml(new TagBuilder("i").Class("fa fa-save"));

            return submit.ToHtml();

        }
    }
}
