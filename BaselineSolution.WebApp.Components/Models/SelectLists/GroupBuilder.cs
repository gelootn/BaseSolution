using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using BaselineSolution.Framework.Infrastructure.Attributes;

namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    /// <summary>
    /// Builder class for where the grouping has already been done
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TGroup"></typeparam>
    /// <typeparam name="TOption"></typeparam>
    public class GroupBuilder <TModel, TProperty, TGroup, TOption> : IGroupBuilder<TGroup, TOption>
    {
        [NotNull] private readonly IEnumerable<IGrouping<TGroup, TOption>> _groupedOptions;
        [NotNull] private readonly HtmlHelper<TModel> _html;
        [CanBeNull] private readonly object _htmlAttributes;
        [CanBeNull] private readonly string _optionLabel;
        [NotNull] private readonly Expression<Func<TModel, TProperty>> _propertyExpression;

        public GroupBuilder([NotNull] HtmlHelper<TModel> html,
                            [NotNull] Expression<Func<TModel, TProperty>> propertyExpression,
                            [NotNull] IEnumerable<IGrouping<TGroup, TOption>> groupedOptions,
                            [CanBeNull] string optionLabel,
                            [CanBeNull] object htmlAttributes)
        {
            _html = html;
            _propertyExpression = propertyExpression;
            _groupedOptions = groupedOptions;
            _optionLabel = optionLabel;
            _htmlAttributes = htmlAttributes;
        }

        public IOptionsBuilder<TOption> Group(Func<TGroup, IEnumerable<TOption>, string> groupLabelFunction, Func<TGroup, IEnumerable<TOption>, object> groupHtmlAttributesFunction)
        {
            return new GroupedOptionsBuilder<TModel, TProperty, TGroup, TOption>(_html,
                                             _propertyExpression,
                                             _optionLabel,
                                             _htmlAttributes,
                                             _groupedOptions,
                                             groupLabelFunction,
                                             groupHtmlAttributesFunction);
        }

        public IOptionsBuilder<TOption> Group(Func<TGroup, IEnumerable<TOption>, string> groupLabelFunction)
        {
            return Group(groupLabelFunction, (Func<TGroup, IEnumerable<TOption>, object>)null);
        }

        public IOptionsBuilder<TOption> Group(Func<TGroup, string> groupLabelFunction, Func<TGroup, object> groupHtmlAttributesFunction)
        {
            return Group((group, options) => groupLabelFunction(group), (groupHtmlAttributesFunction != null ? (group, options) => groupHtmlAttributesFunction(group) : (Func<TGroup, IEnumerable<TOption>, object>) null));
        }

        public IOptionsBuilder<TOption> Group(Func<TGroup, string> groupLabelFunction)
        {
            return Group(groupLabelFunction, (Func<TGroup, object>) null);
        }

        public IOptionsBuilder<TOption> Group()
        {
            return Group(group => Convert.ToString(group), (Func<TGroup, object>) null);
        }
    }
}