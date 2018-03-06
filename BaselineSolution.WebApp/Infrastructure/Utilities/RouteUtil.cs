using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BaselineSolution.WebApp.Infrastructure.Utilities
{
   public static class RouteUtil
    {
        /// <summary>
        /// Returns a list of methods per controller per area as dictionaries.
        /// The main dictionary contains all the areas as strings.
        /// The values are dictionaries themselves, which contain the Controller type as key and an IEnumerable of MethodInfo as value
        /// </summary>
        /// <param name="namespacePrefix"></param>
        /// <returns></returns>
        public static Dictionary<string, Dictionary<Type, IEnumerable<MethodInfo>>> Explore(string namespacePrefix)
        {


            try
            {
                var routeBases = RouteTable.Routes;
                var routes = routeBases.Select(r => r as Route);
                var dataTokens = routes.Where(r => r != null && r.DataTokens != null).Select(r => r.DataTokens);
                var areas = dataTokens.Where(d => d.ContainsKey("area")).Select(d => d["area"].ToString()).Distinct();

                var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                var controllerTypes = assemblies
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t != null
                        && t.IsPublic
                        && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                        && !t.IsAbstract
                        && typeof(IController).IsAssignableFrom(t)
                        && t.FullName.StartsWith(namespacePrefix));

                var controllerMethods = controllerTypes.ToDictionary(
                    controllerType => controllerType,
                    controllerType => controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)));

                var areaControllers = areas.ToDictionary(
                    area => area,
                    area => controllerMethods.Where(c => c.Key.FullName.StartsWith(namespacePrefix + ".Areas." + area)).ToDictionary(c => c.Key, c => c.Value));

                return areaControllers;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Analyzes a given route and extracts the area, controller and action as a dictionary
        /// </summary>
        /// <param name="route"></param>
        /// <returns>A dictionary containing values for each <see cref="RouteKey"/></returns>
        public static IDictionary<RouteKey, string> Analyze(string route)
        {
            var requestUrl = HttpContext.Current.Request.Url;
            var url = string.Format("{0}://{1}{2}", requestUrl.Scheme, requestUrl.Authority, route);
            var uri = new Uri(url);
            var routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            var routeData = routeInfo.RouteData;

            var results = new Dictionary<RouteKey, string>
                {
                    {RouteKey.Area, routeData.DataTokens["area"].ToString()},
                    {RouteKey.Controller, routeData.Values["controller"].ToString()},
                    {RouteKey.Action, routeData.Values["action"].ToString()}
                };

            return results;
        }

        public static object ExploreToJson(string namespacePrefix)
        {
            var exploreResult = Explore(namespacePrefix);
            return new
            {
                Areas = exploreResult.Select(area => new
                {
                    Area = area.Key,
                    Controllers = area.Value.Select(controller => new
                    {
                        Controller = controller.Key.Name,
                        Actions = controller.Value.GroupBy(method => method.Name).First().Select(method => new
                        {
                            Action = method.Name
                        })
                    })
                })
            };
        }
    }
}