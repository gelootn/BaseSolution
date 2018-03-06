using System;
using System.Web;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.WebApp.Infrastructure.Extensions;

namespace BaselineSolution.WebApp.Infrastructure.Utilities
{
    internal static class RouteInfoExtensions
    {
        public static bool IsRouteMatch(this Uri uri, string controllerName, string actionName)
        {
            var routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            return (routeInfo.RouteData.GetController() == controllerName && routeInfo.RouteData.GetAction() == actionName);
        }

        public static string GetRouteParameterValue(this Uri uri, string parameterName)
        {
            var routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            return routeInfo.RouteData.Values[parameterName] != null ? routeInfo.RouteData.Values[parameterName].ToString() : null;
        }
    }
}