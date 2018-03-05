using System.Collections.Generic;
using System.Web.Mvc;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.WebApp.Components.Models.SelectLists
{
    public interface ISelectableBuilder
    {
        /// <summary>
        ///     Configures this select list for the type <typeparamref name="TSelectable"/>
        /// </summary>
        /// <typeparam name="TSelectable">The type of the selectable</typeparam>
        /// <returns></returns>
        MvcHtmlString ToHtml<TSelectable>() where TSelectable: ISelectable;

        /// <summary>
        ///     Configures this select list for the type <typeparamref name="TSelectable"/>
        /// </summary>
        /// <typeparam name="TSelectable">The type of the selectable</typeparam>
        /// <param name="htmlAttributes">The extra html attributes to be applied to the hidden input tag</param>
        /// <returns></returns>
        MvcHtmlString ToHtml<TSelectable>(object htmlAttributes) where TSelectable: ISelectable;


        /// <summary>
        ///     Configures this select list with the given <paramref name="url"/>
        /// </summary>
        /// <param name="url">The url that points to an action that will handle the select ajax requests</param>
        /// <returns></returns>
        MvcHtmlString ToHtml(string url);

        /// <summary>
        ///     Configures this select list with the given <paramref name="url"/>
        /// </summary>
        /// <param name="url">The url that points to an action that will handle the select ajax requests</param>
        /// <param name="htmlAttributes">The extra html attributes to be applied to the hidden input tag</param>
        /// <returns></returns>
        MvcHtmlString ToHtml(string url, object htmlAttributes);

        /// <summary>
        ///     Configures this select list with the given <paramref name="url"/>
        /// </summary>
        /// <param name="url">The url that points to an action that will handle the select ajax requests</param>
        /// <param name="htmlAttributes">The extra html attributes to be applied to the hidden input tag</param>
        /// <returns></returns>
        MvcHtmlString ToHtml(string url, IDictionary<string, object> htmlAttributes);
    }
}
