using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Utilities;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    public class OptionsBuilder<TModel, TProperty, TOption> : IOptionsBuilder<TOption>
    {
        [NotNull]
        private readonly HtmlHelper<TModel> _html;

        [NotNull]
        private readonly Expression<Func<TModel, TProperty>> _propertyExpression;

        [NotNull]
        private readonly IEnumerable<TOption> _options;

        private readonly LocalSelectOptions _selectOptions;

        public OptionsBuilder(HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> propertyExpression, IEnumerable<TOption> options, LocalSelectOptions selectOptions)
        {
            _html = html;
            _options = options;
            _propertyExpression = propertyExpression;
            _selectOptions = selectOptions;
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
            if (_options == null)
                throw new NullReferenceException("options");
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
            if (_selectOptions.Placeholder != null)
            {
                var optionTag =
                    new TagBuilder("option").Attribute("value", string.Empty).Html(_selectOptions.Placeholder);
                if (string.Equals(Convert.ToString(propertyValueString), string.Empty))
                {
                    optionTag.MergeAttribute("selected", "selected");
                }
                selectTag.AppendHtml(optionTag);
            }

            // add options
            foreach (var option in _options)
            {
                // get properties for this option
                string value = optionValueFunction(option);
                string text = optionTextFunction(option);
                bool isSelected = string.Equals(Convert.ToString(propertyValueString), value)
                        || Equals(propertyValue, option)
                        || ReferenceEquals(propertyValue, option)
                        || Enums.EnumEquals(propertyValue, value);

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

                // append option tag to select tag
                selectTag.AppendHtml(optionTag);
            }

            // add unobtrusive validation attributes
            selectTag.Merge(_html.GetUnobtrusiveValidationAttributes(propertyName, ModelMetadata.FromLambdaExpression(_propertyExpression, _html.ViewData)));

            // add html attributes for the select tag
            if (_selectOptions.HtmlAttributes != null)
            {
                selectTag.Merge(_selectOptions.HtmlAttributes);
            }

            //if (!string.IsNullOrWhiteSpace(_selectOptions.Placeholder))
            //{
            //    selectTag.MergeAttribute("data-placeholdertext", _selectOptions.Placeholder);
            //}

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