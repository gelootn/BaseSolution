using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.WebApp.Components.Models.SelectLists;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Forms
{
    /// <summary>
    /// Custom html helpers to create select lists
    /// </summary>
    public static class SelectForHelper
    {
        #region Local data

        #region Generic IEnumerable<TOption> support (returns IOptionsBuilder)

        /// <summary>
        ///     <para>
        ///         Returns an IOptionsBuilder instance to further configure the selectlist.
        ///     </para>
        ///     <para>
        ///         If one of the <paramref name="options"/> has a value which is equal to the property from the <paramref name="propertyExpression"/>,
        ///         this option will be preselected.         
        ///     </para>
        /// </summary>
        ///  
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the value</typeparam>
        /// <typeparam name="TOption">The type of the options</typeparam>
        /// 
        /// <param name="html">The HTML helper instance that this method extends</param>
        /// <param name="propertyExpression">An expression that identifies the property for which this select is made</param>
        /// <param name="options">The options</param>
        /// <param name="selectOptions">The select options that contain optional parameters for this element</param>
        /// <returns>
        ///     An IOptionsBuilder instance to further configure the selectlist.
        /// </returns>
        public static IOptionsBuilder<TOption> SelectFor<TModel, TProperty, TOption>(
            [NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>>
                propertyExpression,
            [NotNull] IEnumerable<TOption> options,
            [NotNull] LocalSelectOptions selectOptions)
        {
            return new OptionsBuilder<TModel, TProperty, TOption>(html,
                                                                  propertyExpression,
                                                                  options,
                                                                  selectOptions);
        }

        /// <summary>
        ///     <para>
        ///         Returns an IOptionsBuilder instance to further configure the selectlist.
        ///     </para>
        ///     <para>
        ///         If one of the <paramref name="options"/> has a value which is equal to the property from the <paramref name="propertyExpression"/>,
        ///         this option will be preselected.         
        ///     </para>
        /// </summary>
        ///  
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the value</typeparam>
        /// <typeparam name="TOption">The type of the options</typeparam>
        /// 
        /// <param name="html">The HTML helper instance that this method extends</param>
        /// <param name="propertyExpression">An expression that identifies the property for which this select is made</param>
        /// <param name="options">The options</param>
        /// 
        /// <returns>
        ///     An IOptionsBuilder instance to further configure the selectlist.
        /// </returns>
        public static IOptionsBuilder<TOption> SelectFor<TModel, TProperty, TOption>(
            [NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>>
                propertyExpression,
            [NotNull] IEnumerable<TOption> options)
        {
            return SelectFor(html, propertyExpression, options, new LocalSelectOptions());
        }

        #endregion

        #region IEnumerable<SelectListItem> support (returns MvcHtmlString)

        /// <summary>
        ///     <para>
        ///         Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///         with a list of <paramref name="selectListItems"/>.<br/>
        ///         The <paramref name="selectListItems"/> of which the value equals the property will be preselected.
        ///     </para>
        /// </summary>
        /// <example>
        ///     <code>
        ///         @Html.SelectListFor(model => model.User.AccountId,                              
        ///                             Model.Accounts,                                           
        ///                             "--- Select an account ---",
        ///                             new { title = "Selectlist for accounts" })
        ///     </code>
        /// </example>
        /// 
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the value</typeparam>
        /// 
        /// <param name="html">The HTML helper instance that this method extends</param>
        /// <param name="propertyExpression">An expression that identifies the property for which this select is made</param>
        /// <param name="selectListItems">The items that will be used to populate the options</param>
        /// <param name="selectOptions">The select options that contain optional parameters for this element</param>
        /// <returns>
        ///     Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///     with a list of <paramref name="selectListItems"/>.<br/>
        ///     The <paramref name="selectListItems"/> of which the value equals the property will be preselected.
        /// </returns>
        public static MvcHtmlString SelectFor<TModel, TProperty>([NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>>
                propertyExpression,
            [NotNull] IEnumerable<SelectListItem> selectListItems,
            [NotNull] LocalSelectOptions selectOptions)
        {
            return SelectFor<TModel, TProperty, SelectListItem>(html,
                propertyExpression,
                selectListItems,
                selectOptions)
                .Options(item => item.Value,
                    item => item.Text,
                    (Func<SelectListItem, object>)null);
        }

        /// <summary>
        ///     <para>
        ///         Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///         with a list of <paramref name="selectListItems"/>.<br/>
        ///         The <paramref name="selectListItems"/> of which the value equals the property will be preselected.
        ///     </para>
        /// </summary>
        /// <example>
        ///     <code>
        ///         @Html.SelectListFor(model => model.User.AccountId,                              
        ///                             Model.Accounts)
        ///     </code>
        /// </example>
        /// 
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the value</typeparam>
        /// 
        /// <param name="html">The HTML helper instance that this method extends</param>
        /// <param name="propertyExpression">An expression that identifies the property for which this select is made</param>
        /// <param name="selectListItems">The items that will be used to populate the options</param>
        /// 
        /// <returns>
        ///     Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///     with a list of <paramref name="selectListItems"/>.<br/>
        ///     The <paramref name="selectListItems"/> of which the value equals the property will be preselected.
        /// </returns>
        public static MvcHtmlString SelectFor<TModel, TProperty>([NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>>
                propertyExpression,
            [NotNull] IEnumerable<SelectListItem> selectListItems)
        {
            return SelectFor(html,
                propertyExpression,
                selectListItems,
                new LocalSelectOptions());
        }
        
        #endregion

        #region IEnumerable<ISelectable> support (returns MvcHtmlString)

        /// <summary>
        ///     <para>
        ///         Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///         with a list of <paramref name="selectables"/>.<br/>
        ///         The <paramref name="selectables"/> of which the value equals the property will be preselected.
        ///     </para>
        /// </summary>
        /// <example>
        ///     <code>
        ///         @Html.SelectListFor(model => model.User.AccountId,                              
        ///                             Model.Accounts,                                           
        ///                             "--- Select an account ---",
        ///                             new { title = "Selectlist for accounts" })
        ///     </code>
        /// </example>
        /// 
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the value</typeparam>
        /// 
        /// <param name="html">The HTML helper instance that this method extends</param>
        /// <param name="propertyExpression">An expression that identifies the property for which this select is made</param>
        /// <param name="selectables">The items that will be used to populate the options</param>
        /// <param name="selectOptions">The select options that contain optional parameters for this element</param>
        /// <returns>
        ///     Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///     with a list of <paramref name="selectables"/>.<br/>
        ///     The <paramref name="selectables"/> of which the value equals the property will be preselected.
        /// </returns>
        public static MvcHtmlString SelectFor<TModel, TProperty>([NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>>
                propertyExpression,
            [NotNull] IEnumerable<ISelectable> selectables,
            [NotNull] LocalSelectOptions selectOptions) 
        {
            return SelectFor<TModel, TProperty, ISelectable>(html,
                propertyExpression,
                selectables,
                selectOptions)
                .Options(item => item.Key,
                    item => item.Name,
                    (Func<ISelectable, object>)null);
        }

        /// <summary>
        ///     <para>
        ///         Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///         with a list of <paramref name="selectListItems"/>.<br/>
        ///         The <paramref name="selectListItems"/> of which the value equals the property will be preselected.
        ///     </para>
        /// </summary>
        /// <example>
        ///     <code>
        ///         @Html.SelectListFor(model => model.User.AccountId,                              
        ///                             Model.Accounts)
        ///     </code>
        /// </example>
        /// 
        /// <typeparam name="TModel">The type of the model</typeparam>
        /// <typeparam name="TProperty">The type of the value</typeparam>
        /// 
        /// <param name="html">The HTML helper instance that this method extends</param>
        /// <param name="propertyExpression">An expression that identifies the property for which this select is made</param>
        /// <param name="selectListItems">The items that will be used to populate the options</param>
        /// 
        /// <returns>
        ///     Returns an HTML select element for the property specified by the <paramref name="propertyExpression"/> 
        ///     with a list of <paramref name="selectListItems"/>.<br/>
        ///     The <paramref name="selectListItems"/> of which the value equals the property will be preselected.
        /// </returns>
        public static MvcHtmlString SelectFor<TModel, TProperty>([NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>>
                propertyExpression,
            [NotNull] IEnumerable<ISelectable> selectListItems)
        {
            return SelectFor(html,
                propertyExpression,
                selectListItems,
                new LocalSelectOptions());
        }
        
        #endregion

        #endregion

        #region Remote data

        public static ISelectableBuilder SelectFor<TModel, TProperty>([NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>> propertyExpression,
            [NotNull] RemoteSelectOptions selectOptions)
        {
            selectOptions.Placeholder = selectOptions.Placeholder ?? html.DisplayNameFor(propertyExpression).ToHtmlString();
            return new SelectableBuilder<TModel, TProperty>(html, propertyExpression, selectOptions);
        }

        public static ISelectableBuilder SelectFor<TModel, TProperty>([NotNull] this HtmlHelper<TModel> html,
            [NotNull] Expression<Func<TModel, TProperty>> propertyExpression)
        {
            return SelectFor(html, propertyExpression, new RemoteSelectOptions());
        }

        #endregion
    }
}
