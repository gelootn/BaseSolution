using System;
using System.Web.Mvc;
using BaselineSolution.Framework.Infrastructure.Attributes;

namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    public interface IOptionsBuilder<out TOption>
    {
        /// <summary>
        ///     Returns the select list with one or more options preselected where the option value is equal to the property value.
        /// </summary>
        /// <param name="optionValueFunction">The function that takes an <typeparamref name="TOption"/> and returns the value that should be used for the option tag</param>
        /// <param name="optionTextFunction">The function that takes an <typeparamref name="TOption"/> and returns the text that should be used for the option tag</param>
        /// <param name="optionHtmlAttributesFunction">The function that takes an <typeparamref name="TOption"/> and returns the html attributes that should be set on the option tag</param>
        /// <returns>The select list with one or more options preselected where the option value is equal to the property value.</returns>
        [NotNull]
        MvcHtmlString Options([NotNull] Func<TOption, string> optionValueFunction,
                              [NotNull] Func<TOption, string> optionTextFunction,
                              [CanBeNull] Func<TOption, object> optionHtmlAttributesFunction);

        /// <summary>
        ///     Returns the select list with one or more options preselected where the option value is equal to the property value.
        /// </summary>
        /// <param name="optionValueFunction">The function that takes an <typeparamref name="TOption"/> and returns the value that should be used for the option tag</param>
        /// <param name="optionTextFunction">The function that takes an <typeparamref name="TOption"/> and returns the text that should be used for the option tag</param>
        /// <returns>The select list with one or more options preselected where the option value is equal to the property value.</returns>
        [NotNull]
        MvcHtmlString Options([NotNull] Func<TOption, string> optionValueFunction,
                              [NotNull] Func<TOption, string> optionTextFunction);

        /// <summary>
        ///     Returns the select list with one or more options preselected where the option value is equal to the property value.
        /// </summary>
        /// <param name="optionValueFunction">The function that takes an <typeparamref name="TOption"/> and returns the value that should be used for the option tag</param>
        /// <param name="optionTextFunction">The function that takes an <typeparamref name="TOption"/> and returns the text that should be used for the option tag</param>
        /// <param name="optionHtmlAttributesFunction">The function that takes an <typeparamref name="TOption"/> and returns the html attributes that should be set on the option tag</param>
        /// <returns>The select list with one or more options preselected where the option value is equal to the property value.</returns>
        [NotNull]
        MvcHtmlString Options([NotNull] Func<TOption, int> optionValueFunction,
                              [NotNull] Func<TOption, string> optionTextFunction,
                              [CanBeNull] Func<TOption, object> optionHtmlAttributesFunction);

        /// <summary>
        ///     Returns the select list with one or more options preselected where the option value is equal to the property value.
        /// </summary>
        /// <param name="optionValueFunction">The function that takes an <typeparamref name="TOption"/> and returns the value that should be used for the option tag</param>
        /// <param name="optionTextFunction">The function that takes an <typeparamref name="TOption"/> and returns the text that should be used for the option tag</param>
        /// <returns>The select list with one or more options preselected where the option value is equal to the property value.</returns>
        [NotNull]
        MvcHtmlString Options([NotNull] Func<TOption, int> optionValueFunction,
                              [NotNull] Func<TOption, string> optionTextFunction);
    }
}