using System;
using System.Collections.Generic;
using BaselineSolution.Framework.Infrastructure.Attributes;

namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    public interface IGroupBuilder <out TGroup, out TOption>
    {
        /// <summary>
        ///     Returns a grouped option builder to further configure the select list.<br/>
        ///     <strong>If no groupLabelFunction is provided, the group key will be used as the label.</strong>
        /// </summary>
        /// <returns>
        ///     A grouped option builder that will use the group key as the label to further configure the select list. <br/>
        ///     <strong>If no groupLabelFunction is provided, the group key will be used as the label.</strong>
        /// </returns>
        [NotNull]
        IOptionsBuilder<TOption> Group();

        /// <summary>
        ///     Returns a grouped option builder to further configure the select list
        /// </summary>
        /// <param name="groupLabelFunction">The function that takes an <typeparamref name="TGroup"/> and returns the label that should be used for the optgroup tag</param>
        /// <returns>
        ///     A grouped option builder to further configure the select list
        /// </returns>
        [NotNull]
        IOptionsBuilder<TOption> Group([NotNull] Func<TGroup, string> groupLabelFunction);

        /// <summary>
        ///     Returns a grouped option builder to further configure the select list
        /// </summary>
        /// <param name="groupLabelFunction">The function that takes an <typeparamref name="TGroup"/> and returns the label that should be used for the optgroup tag</param>
        /// <param name="groupHtmlAttributesFunction">The function that takes an <typeparamref name="TGroup"/> and returns the html attributes that should be set on the optgroup tag</param>
        /// <returns>
        ///     A grouped option builder to further configure the select list
        /// </returns>
        [NotNull]
        IOptionsBuilder<TOption> Group([NotNull] Func<TGroup, string> groupLabelFunction,
                                       [CanBeNull] Func<TGroup, object> groupHtmlAttributesFunction);


        /// <summary>
        ///     Returns a grouped option builder to further configure the select list
        /// </summary>
        /// <param name="groupLabelFunction">The function that takes an <typeparamref name="TGroup"/> and an enumerable of <typeparamref name="TOption"/> and returns the label that should be used for the optgroup tag</param>
        /// <returns>
        ///     A grouped option builder to further configure the select list
        /// </returns>
        [NotNull]
        IOptionsBuilder<TOption> Group([NotNull] Func<TGroup, IEnumerable<TOption>, string> groupLabelFunction);

        /// <summary>
        ///     Returns a grouped option builder to further configure the select list
        /// </summary>
        /// <param name="groupLabelFunction">The function that takes an <typeparamref name="TGroup"/> and an enumerable of <typeparamref name="TOption"/> and returns the label that should be used for the optgroup tag</param>
        /// <param name="groupHtmlAttributesFunction">The function that takes an <typeparamref name="TGroup"/> and an enumerable of <typeparamref name="TOption"/> and returns the html attributes that should be set on the optgroup tag</param>
        /// <returns>
        ///     A grouped option builder to further configure the select list
        /// </returns>
        [NotNull]
        IOptionsBuilder<TOption> Group([NotNull] Func<TGroup, IEnumerable<TOption>, string> groupLabelFunction,
                                       [CanBeNull] Func<TGroup, IEnumerable<TOption>, object> groupHtmlAttributesFunction);


    }
}