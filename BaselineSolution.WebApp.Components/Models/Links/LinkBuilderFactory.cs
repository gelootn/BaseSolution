using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Components.Extensions;

namespace BaselineSolution.WebApp.Components.Models.Links
{
    public static class LinkBuilderFactory
    {
        /// <summary>
        ///     Generates an empty '#' url that is always allowed
        /// </summary>
        /// <returns>
        ///     An empty '#' url that is always allowed
        /// </returns>
        public static ILinkBuilder Create()
        {
            return new LinkBuilder("#", null, null, null, true, null);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="requestContext">The request context</param>
        /// <param name="actionName">The name of the action method.</param>
        public static ILinkBuilder Create(RequestContext requestContext, [AspMvcAction] string actionName)
        {
            return Create(requestContext, actionName, null, null);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="requestContext">The request context</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        public static ILinkBuilder Create(RequestContext requestContext, [AspMvcAction] string actionName, object routeValues)
        {
            return Create(requestContext, actionName, null, new RouteValueDictionary(routeValues));
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method for the specified action name and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="requestContext">The request context</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        public static ILinkBuilder Create(RequestContext requestContext,
                                        [AspMvcAction] string actionName,
                                        RouteValueDictionary routeValues)
        {
            return Create(requestContext, actionName, null, routeValues);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name and controller name.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="requestContext">The request context</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        public static ILinkBuilder Create(RequestContext requestContext,
                                        [AspMvcAction] string actionName,
                                        [AspMvcController] string controllerName)
        {
            return Create(requestContext, actionName, controllerName, null);
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name, controller name, and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="requestContext">The request context</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object that contains the parameters for a route. The parameters are retrieved through reflection by examining the properties of the object. The object is typically created by using object initializer syntax.</param>
        public static ILinkBuilder Create(RequestContext requestContext,
                                        [AspMvcAction] string actionName,
                                        [AspMvcController] string controllerName,
                                        object routeValues)
        {
            return Create(requestContext, actionName, controllerName, new RouteValueDictionary(routeValues));
        }

        /// <summary>
        ///     Generates a fully qualified URL to an action method by using the specified action name, controller name, and route values.
        /// </summary>
        /// <returns>
        ///     The fully qualified URL to an action method.
        /// </returns>
        /// <param name="requestContext">The request context</param>
        /// <param name="actionName">The name of the action method.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="routeValues">An object that contains the parameters for a route.</param>
        public static ILinkBuilder Create(RequestContext requestContext,
                                        [AspMvcAction] string actionName,
                                        [AspMvcController] string controllerName,
                                        RouteValueDictionary routeValues)
        {
            var url = new UrlHelper(requestContext);
            return GenerateUrl(null, actionName, controllerName, routeValues, url.RouteCollection, url.RequestContext, true);
        }

        /// <summary>
        ///     Returns a string that contains a URL.
        /// </summary>
        /// <returns>
        ///     A string that contains a URL.
        /// </returns>
        /// <param name="routeName">The route name.</param>
        /// <param name="actionName">The action name.</param>
        /// <param name="controllerName">The controller name.</param>
        /// <param name="routeValues">The route values.</param>
        /// <param name="routeCollection">The route collection.</param>
        /// <param name="requestContext">The request context.</param>
        /// <param name="includeImplicitMvcValues">true to include implicit MVC values; otherwise. false.</param>
        private static ILinkBuilder GenerateUrl(string routeName,
                                                string actionName,
                                                string controllerName,
                                                RouteValueDictionary routeValues,
                                                RouteCollection routeCollection,
                                                RequestContext requestContext,
                                                bool includeImplicitMvcValues)
        {
            var routeValueDictionary = MergeRouteValues(actionName,
                                                        controllerName,
                                                        requestContext.RouteData.Values,
                                                        routeValues,
                                                        includeImplicitMvcValues);
            if (requestContext.HttpContext.User == null)
            {
                requestContext = HttpContext.Current.Request.RequestContext;
            }
            var area = routeValueDictionary["area"] as string ?? requestContext.RouteData.GetArea();
            var controller = routeValueDictionary["controller"] as string ?? requestContext.RouteData.GetController();
            var action = routeValueDictionary["action"] as string ?? requestContext.RouteData.GetAction();
            var isAllowed = requestContext.Authenticate(area, controller, action);
            var url = UrlHelper.GenerateUrl(routeName,
                                               actionName,
                                               controllerName,
                                               routeValues,
                                               routeCollection,
                                               requestContext,
                                               includeImplicitMvcValues);
            if (url == null)
            {
                throw new InvalidOperationException(
                    string.Format("Could not resolve url from the following data: [ {0} ]",
                                    string.Join(",", routeValueDictionary.Select(kvp => kvp.Key + " = " + kvp.Value))));
            }
            string absoluteUrl = null;
            if (requestContext.HttpContext.Request.Url != null)
            {
                absoluteUrl = UrlHelper.GenerateUrl(routeName, actionName, controllerName, requestContext.HttpContext.Request.Url.Scheme, null, null, routeValues, routeCollection, requestContext, true);
            }
            return new LinkBuilder(url, area, controller, action, isAllowed, absoluteUrl ?? string.Empty);
        }

        /// <summary>
        ///     Merges the explicit routevalues with the implicit routevalues
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="implicitRouteValues"></param>
        /// <param name="routeValues"></param>
        /// <param name="includeImplicitMvcValues"></param>
        /// <returns></returns>
        private static RouteValueDictionary MergeRouteValues(string actionName,
                                                             string controllerName,
                                                             RouteValueDictionary implicitRouteValues,
                                                             RouteValueDictionary routeValues,
                                                             bool includeImplicitMvcValues)
        {
            var routeValueDictionary = new RouteValueDictionary();
            if (includeImplicitMvcValues)
            {
                object obj;
                if (implicitRouteValues != null && implicitRouteValues.TryGetValue("action", out obj))
                    routeValueDictionary["action"] = obj;
                if (implicitRouteValues != null && implicitRouteValues.TryGetValue("controller", out obj))
                    routeValueDictionary["controller"] = obj;
            }
            if (routeValues != null)
            {
                foreach (var keyValuePair in new RouteValueDictionary(routeValues))
                    routeValueDictionary[keyValuePair.Key] = keyValuePair.Value;
            }
            if (actionName != null)
                routeValueDictionary["action"] = actionName;
            if (controllerName != null)
                routeValueDictionary["controller"] = controllerName;
            return routeValueDictionary;
        }
    }
}
