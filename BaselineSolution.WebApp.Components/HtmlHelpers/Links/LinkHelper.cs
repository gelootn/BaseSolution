using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Components.Models.Links;

namespace BaselineSolution.WebApp.Components.HtmlHelpers.Links
{
    /// <summary>
    ///     Automatically hides or shows links based on authentication
    /// </summary>
    public static class LinkHelper
    {
        /// <summary>
        ///     Generates an empty '#' url that is always allowed
        /// </summary>
        /// <param name="html">The html helper</param>
        /// <returns>
        ///     An empty '#' url that is always allowed
        /// </returns>
        public static ILinkBuilder Link(this HtmlHelper html)
        {
            return LinkBuilderFactory.Create();
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="html">The html helper</param>
        /// <param name="actionName">The name of the action method.</param>
        public static ILinkBuilder Link(this HtmlHelper html, [AspMvcAction] string actionName)
        {
            return LinkBuilderFactory.Create(html.ViewContext.RequestContext, actionName);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="html">The html helper</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        public static ILinkBuilder Link(this HtmlHelper html, [AspMvcAction] string actionName, object routeValues)
        {
            return LinkBuilderFactory.Create(html.ViewContext.RequestContext, actionName, routeValues);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method for the specified action name and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="html">The html helper</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        public static ILinkBuilder Link(this HtmlHelper html,
                                        [AspMvcAction] string actionName,
                                        RouteValueDictionary routeValues)
        {
            return LinkBuilderFactory.Create(html.ViewContext.RequestContext, actionName, routeValues);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name and controller name.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="html">The html helper</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        public static ILinkBuilder Link(this HtmlHelper html,
                                        [AspMvcAction] string actionName,
                                        [AspMvcController] string controllerName)
        {
            return LinkBuilderFactory.Create(html.ViewContext.RequestContext, actionName, controllerName);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name, controller name, and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="html">The html helper</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        public static ILinkBuilder Link(this HtmlHelper html,
                                        [AspMvcAction] string actionName,
                                        [AspMvcController] string controllerName,
                                        object routeValues)
        {
            return LinkBuilderFactory.Create(html.ViewContext.RequestContext, actionName, controllerName, routeValues);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name, controller name, and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="html">The html helper</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        public static ILinkBuilder Link(this HtmlHelper html,
                                        [AspMvcAction] string actionName,
                                        [AspMvcController] string controllerName,
                                        RouteValueDictionary routeValues)
        {
            return LinkBuilderFactory.Create(html.ViewContext.RequestContext, actionName, controllerName, routeValues);
        }
    }
}
