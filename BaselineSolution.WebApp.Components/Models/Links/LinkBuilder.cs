using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.Models.Links
{
    [DebuggerDisplay("IsAllowed = {IsAllowed}, Link = {Link}")]
    public class LinkBuilder : ILinkBuilder
    {
        private readonly string _actionName;
        private readonly string _areaName;
        private readonly string _controllerName;
        private readonly string _absoluteUrl;

        private readonly bool _isAllowed;
        private readonly string _link;
        private readonly Stack<TagBuilder> _surroundingTags;


        public LinkBuilder(string link, string areaName, string controllerName, string actionName, bool isAllowed,
            string absoluteUrl)
        {
            _link = link;
            _areaName = areaName;
            _controllerName = controllerName;
            _actionName = actionName;
            _isAllowed = isAllowed;
            _absoluteUrl = absoluteUrl;

            _surroundingTags = new Stack<TagBuilder>();
        }

        /// <summary>
        ///     Gets the action name of the link
        /// </summary>
        public string ActionName
        {
            get { return _actionName; }
        }

        public string AbsoluteUrl
        {
            get { return _absoluteUrl; }
        }

        public string Link
        {
            get { return _link; }
        }

        public bool IsAllowed
        {
            get { return _isAllowed; }
        }

        /// <summary>
        ///     Gets the controller name of the link
        /// </summary>
        public string ControllerName
        {
            get { return _controllerName; }
        }

        /// <summary>
        ///     Gets the area name of the link
        /// </summary>
        public string AreaName
        {
            get { return _areaName; }
        }

        /// <summary>
        ///     Returns a link with a simple label
        /// </summary>
        /// <param name="labelText"></param>
        /// <returns></returns>
        public MvcHtmlString ToHtml(string labelText)
        {
            return ToHtml(labelText, null, null);
        }

        /// <summary>
        ///     Returns a link with a label and an icon
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="iconClass"></param>
        /// <returns></returns>
        public MvcHtmlString ToHtml(string labelText, string iconClass)
        {
            return ToHtml(labelText, iconClass, null);
        }

        /// <summary>
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public MvcHtmlString ToHtml(string labelText, object htmlAttributes)
        {
            return ToHtml(labelText, null, htmlAttributes);
        }

        /// <summary>
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="iconClass"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public MvcHtmlString ToHtml(string labelText, string iconClass, object htmlAttributes)
        {
            return this.ToHtml(
                l =>
                {
                    var a = new TagBuilder("a");
                    if (!string.IsNullOrWhiteSpace(iconClass))
                    {
                        TagBuilder icon = new TagBuilder("i").Class(iconClass);
                        a.AppendHtml(icon);
                        labelText = String.IsNullOrWhiteSpace(labelText)
                            ? string.Empty
                            : (labelText.StartsWith(" ")
                                ? labelText
                                : " " + labelText);
                    }
                    a.AppendHtml(String.IsNullOrWhiteSpace(labelText) ? string.Empty : HttpUtility.HtmlEncode(labelText));
                    if (htmlAttributes != null)
                    {
                        RouteValueDictionary attriDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(
                            htmlAttributes);
                        if (!attriDictionary.Any(k => k.Key.Equals("title")))
                        {
                            a.Attribute("title", labelText);
                        }
                        a.MergeAttributes(attriDictionary);
                    }
                    else
                    {
                        a.Attribute("title", labelText);
                    }

                    a.Attribute("href", _link, true);
                    return a.ToHtml();
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public MvcHtmlString ToHtml(Func<ILinkBuilder, MvcHtmlString> func)
        {
            if (!_isAllowed)
                return MvcHtmlString.Create("");

            var html = func(this);
            if (!_surroundingTags.Any()) return html;

            TagBuilder result = _surroundingTags.Pop().Html(html);
            while (_surroundingTags.Count != 0)
            {
                TagBuilder tagBuilder = _surroundingTags.Pop().Html(result);
                result = tagBuilder;
            }

            return result.ToHtml();
        }

        public ILinkBuilder SurroundedBy(TagBuilder tagBuilder)
        {
            _surroundingTags.Push(tagBuilder);
            return this;
        }

        public ILinkBuilder SurroundedBy(string tag)
        {
            return SurroundedBy(new TagBuilder(tag));
        }

        public ILinkBuilder SurroundedBy(string tag, object htmlAttributes)
        {
            var element = new TagBuilder(tag);

            if (htmlAttributes != null)
            {
                RouteValueDictionary attriDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(
                    htmlAttributes);

                element.MergeAttributes(attriDictionary);
            }

            return SurroundedBy(element);
        }

        protected bool Equals(LinkBuilder other)
        {
            return string.Equals(_link, other._link) && _isAllowed.Equals(other._isAllowed);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((LinkBuilder)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_link != null ? _link.GetHashCode() : 0) * 397) ^ _isAllowed.GetHashCode();
            }
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("Area: {0}, Controller: {1}, Action: {2}, Link: {3}, IsAllowed: {4}", AreaName,
                ControllerName, ActionName, Link, IsAllowed);
        }
    }
}
