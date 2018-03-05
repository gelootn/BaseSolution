using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    public class GroupedOptionsBuilder <TModel, TProperty, TGroup, TOption> : IOptionsBuilder<TOption>
    {
        [CanBeNull]
        private readonly Func<TGroup, IEnumerable<TOption>, object> _groupHtmlAttributesFunction;

        [NotNull] 
        private readonly Func<TGroup, IEnumerable<TOption>, string> _groupLabelFunction;

        [NotNull] 
        private readonly IEnumerable<IGrouping<TGroup, TOption>> _groupedOptions;

        [NotNull] 
        private readonly HtmlHelper<TModel> _html;

        [CanBeNull] 
        private readonly object _htmlAttributes;

        [CanBeNull] 
        private readonly string _optionLabel;

        [NotNull] 
        private readonly Expression<Func<TModel, TProperty>> _propertyExpression;


        public GroupedOptionsBuilder([NotNull] HtmlHelper<TModel> html,
                                     [NotNull] Expression<Func<TModel, TProperty>> propertyExpression,
                                     [CanBeNull] string optionLabel,
                                     [CanBeNull] object htmlAttributes,
                                     [NotNull] IEnumerable<IGrouping<TGroup, TOption>> groupedOptions,
                                     [NotNull] Func<TGroup, IEnumerable<TOption>, string> groupLabelFunction,
                                     [CanBeNull] Func<TGroup, IEnumerable<TOption>,  object> groupHtmlAttributesFunction)
        {
            _html = html;
            _propertyExpression = propertyExpression;
            _optionLabel = optionLabel;
            _htmlAttributes = htmlAttributes;
            _groupedOptions = groupedOptions;
            _groupLabelFunction = groupLabelFunction;
            _groupHtmlAttributesFunction = groupHtmlAttributesFunction;
        }

        public MvcHtmlString Options(Func<TOption, string> optionValueFunction,
                                     Func<TOption, string> optionTextFunction,
                                     Func<TOption, object> optionHtmlAttributesFunction)
        {
            // validate parameters
            if (_html == null)
                throw new NullReferenceException("html");
            if (_propertyExpression == null)
                throw new NullReferenceException("propertyExpression");
            if (_groupedOptions == null)
                throw new NullReferenceException("groupedOptions");
            if (_groupLabelFunction == null)
                throw new NullReferenceException("groupLabelFunction");
            if (optionValueFunction == null)
                throw new ArgumentNullException("optionValueFunction");
            if (optionTextFunction == null)
                throw new ArgumentNullException("optionTextFunction");

            // get property information
            var propertyName = _html.NameFor(_propertyExpression).ToString();
            var propertyId = _html.IdFor(_propertyExpression).ToString();
            var propertyValueString = _html.ValueFor(_propertyExpression);
            var propertyValue = ModelMetadata.FromLambdaExpression(_propertyExpression, _html.ViewData).Model;

            // make select tag
            var selectTag = new TagBuilder("select")
                .Attribute("id", propertyId)
                .Attribute("name", propertyName);

            // add optional option label
            if (_optionLabel != null)
            {
                selectTag.AppendHtml(new TagBuilder("option").Html(_optionLabel));
            }

            // add grouped options
            foreach (var group in _groupedOptions)
            {
                // get properties for this group
                TGroup groupKey = group.Key;
                string groupLabel = _groupLabelFunction(groupKey, group);
                object groupHtmlAttributes = _groupHtmlAttributesFunction != null
                                                 ? _groupHtmlAttributesFunction(groupKey, group)
                                                 : null;

                // make optgroup tag
                var optgroupTag = new TagBuilder("optgroup").Attribute("label", groupLabel);
                if (groupHtmlAttributes != null)
                {
                    optgroupTag.Merge(groupHtmlAttributes);
                }

                // add options for this group
                foreach (TOption option in group)
                {
                    string value = optionValueFunction(option);
                    string text = optionTextFunction(option);
                    bool isSelected = string.Equals(Convert.ToString(propertyValueString), value)
                        || Equals(propertyValue, option)
                        || ReferenceEquals(propertyValue, option);

                    // make option tag
                    var optionTag = new TagBuilder("option")
                        .Attribute("value", value)
                        .Html(text);

                    // set selected if value equals property value
                    if (isSelected)
                    {
                        optionTag.MergeAttribute("selected", "selected");
                    }

                    // add html attributes
                    var optionHtmlAttributes = optionHtmlAttributesFunction != null
                                                   ? optionHtmlAttributesFunction(option)
                                                   : null;
                    if (optionHtmlAttributes != null)
                    {
                        optionTag.Merge(optionHtmlAttributes);
                    }

                    // append option tag to optgroup tag
                    optgroupTag.AppendHtml(optionTag);
                }

                // append optgroup tag to select tag
                selectTag.AppendHtml(optgroupTag);
            }

            // add unobtrusive validation attributes
            selectTag.MergeAttributes(_html.GetUnobtrusiveValidationAttributes(propertyName, ModelMetadata.FromLambdaExpression(_propertyExpression, _html.ViewData)));

            // add html attributes for the select tag
            if (_htmlAttributes != null)
            {
                selectTag.Merge(_htmlAttributes);
            }

            // parse to html and return selectlist
            return selectTag.ToHtml();
        }

        public MvcHtmlString Options(Func<TOption, string> optionValueFunction, Func<TOption, string> optionTextFunction)
        {
            return Options(optionValueFunction, optionTextFunction, null);
        }

        public MvcHtmlString Options(Func<TOption, int> optionValueFunction,
                                     Func<TOption, string> optionTextFunction,
                                     Func<TOption, object> optionHtmlAttributesFunction)
        {
            return Options(option => optionValueFunction(option).ToString(CultureInfo.InvariantCulture),
                           optionTextFunction,
                           optionHtmlAttributesFunction);
        }

        public MvcHtmlString Options(Func<TOption, int> optionValueFunction, Func<TOption, string> optionTextFunction)
        {
            return Options(optionValueFunction, optionTextFunction, null);
        }
    }
}