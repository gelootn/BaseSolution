using System;
using System.Web.Mvc;
using BaselineSolution.Framework.Infrastructure.Attributes;

namespace BaselineSolution.WebApp.Components.Models.Links
{
    public interface ILinkBuilder
    {
        /// <summary>
        ///     Gets the relative url of this action
        /// </summary>
        string Link { get; }

        /// <summary>
        ///     Gets a value indicating whether the current user is allowed to access this link.
        /// </summary>
        bool IsAllowed { get; }

        /// <summary>
        ///     Gets the controller name of the link
        /// </summary>
        string ControllerName { get; }

        /// <summary>
        ///     Gets the area name of the link
        /// </summary>
        string AreaName { get; }

        /// <summary>
        ///     Gets the action name of the link
        /// </summary>
        string ActionName { get; }

        /// <summary>
        ///     Gets the absolute url of this action
        /// </summary>
        string AbsoluteUrl { get; }

        /// <summary>
        ///     Returns a link with a simple label
        /// </summary>
        /// <param name="labelText"></param>
        /// <returns></returns>
        MvcHtmlString ToHtml([CanBeNull] string labelText);

        /// <summary>
        ///     Returns a link with a label and an icon
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="iconClass"></param>
        /// <returns></returns>
        MvcHtmlString ToHtml([CanBeNull] string labelText, [CanBeNull] string iconClass);

        /// <summary>
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        MvcHtmlString ToHtml([CanBeNull] string labelText, [CanBeNull] object htmlAttributes);

        /// <summary>
        /// </summary>
        /// <param name="labelText"></param>
        /// <param name="iconClass"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        MvcHtmlString ToHtml([CanBeNull] string labelText, [CanBeNull] string iconClass, [CanBeNull] object htmlAttributes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        MvcHtmlString ToHtml([CanBeNull] Func<ILinkBuilder, MvcHtmlString> func);

        /// <summary>
        /// </summary>
        /// <param name="tagBuilder"></param>
        /// <returns></returns>
        ILinkBuilder SurroundedBy([NotNull] TagBuilder tagBuilder);

        /// <summary>
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        ILinkBuilder SurroundedBy([NotNull] string tag);

        /// <summary>
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        ILinkBuilder SurroundedBy([NotNull] string tag, [CanBeNull] object htmlAttributes);

    }
}
