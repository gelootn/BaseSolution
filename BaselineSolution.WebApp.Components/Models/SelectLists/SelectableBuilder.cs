using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    public class SelectableBuilder<TModel, TProperty>: ISelectableBuilder
    {
        [NotNull]
        private readonly HtmlHelper<TModel> _htmlHelper;

        [NotNull]
        private readonly Expression<Func<TModel, TProperty>> _propertyExpression;

        [NotNull]
        private readonly RemoteSelectOptions _selectOptions;
        
        public SelectableBuilder([NotNull] HtmlHelper<TModel> htmlHelper,
            [NotNull] Expression<Func<TModel, TProperty>> propertyExpression,
            [NotNull] RemoteSelectOptions selectOptions)
        {
            // validate parameters
            if (htmlHelper == null)
                throw new ArgumentNullException("htmlHelper");

            if (propertyExpression == null)
                throw new ArgumentNullException("propertyExpression");
            
            _htmlHelper = htmlHelper;
            _propertyExpression = propertyExpression;
            _selectOptions = selectOptions;
        }

        public MvcHtmlString ToHtml<TSelectable>() where TSelectable : ISelectable
        {
            return ToHtml<TSelectable>(null);
        }

        public MvcHtmlString ToHtml<TSelectable>(object htmlAttributes) where TSelectable : ISelectable
        {
            var urlHelper = new UrlHelper(_htmlHelper.ViewContext.RequestContext);
            var url = urlHelper.Action("Options", "Selectable", new { area = string.Empty });
            var htmlAttributesDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes ?? new {});
            htmlAttributesDictionary["data-type"] = typeof (TSelectable).AssemblyQualifiedName;
            return ToHtml(url, htmlAttributesDictionary);
        }

        public MvcHtmlString ToHtml(string url)
        {
            return ToHtml(url, null);
        }

        public MvcHtmlString ToHtml(string url, object htmlAttributes)
        {
            return ToHtml(url, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes ?? new {}));
        }
        
        public MvcHtmlString ToHtml(string url, IDictionary<string, object> htmlAttributes)
        {
            var propertyName = _htmlHelper.NameFor(_propertyExpression).ToString();
            var propertyId = _htmlHelper.IdFor(_propertyExpression).ToString();
            var propertyValueString = _htmlHelper.ValueFor(_propertyExpression);
            if (propertyValueString != null)
                propertyValueString = new MvcHtmlString(propertyValueString.ToString().Replace("&#39;", "\'").Replace("&#32;", " "));

            var input = new TagBuilder("input")
                .Merge(htmlAttributes)
                .Class(_selectOptions.Multiple ? "enable-remote-multi-select" : "enable-remote-select" )
                .Attribute("type", "hidden")
                .Attribute("data-options-url", url)
                .Attribute("id", propertyId)
                .Attribute("name", propertyName)
                .Attribute("value", propertyValueString);

            if (_selectOptions.CustomInitialize)
            {
                input.Class("custom-initialize");
            }
            if (_selectOptions.Placeholder != null)
            {
                input.Attribute("data-placeholder", _selectOptions.Placeholder);
            }
            if (_selectOptions.SelectOrNew)
            {
                input.Attribute("data-select-or-new", "1");
                input.Attribute("data-select-or-new-url", _selectOptions.AddUrl);
            }
            
            return input.ToHtml();
        }
    }
}
