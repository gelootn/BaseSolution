using System;
using System.Web.Mvc;
using System.Web.Routing;
using BaselineSolution.Framework.Infrastructure.Attributes;

namespace BaselineSolution.Framework.Extensions
{
    public static class ExtensionsForRouteData
    {
        /// <summary>
        /// Extracts the right key out of the routedata, by combining the area name, controller name and action name
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetRightKey(this RouteData @this)
        {
            string area = @this.GetArea();
            string controller = @this.GetController() + "Controller";
            var action = @this.GetAction();
            return string.Join(".", area, controller, action);
        }

        /// <summary>
        ///     Extracts and returns the area from this routedata
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetArea(this RouteData @this)
        {
            string area = null;

            if (@this.DataTokens.ContainsKey("area"))
                area = (string)@this.DataTokens["area"];
            else if (@this.DataTokens.ContainsKey("Area"))
                area = (string)@this.DataTokens["Area"];

            if (@this.Values.ContainsKey("Area"))
                area = (string)@this.Values["Area"];
            else if (@this.Values.ContainsKey("area"))
                area = (string)@this.Values["area"];
            return area;
        }

        /// <summary>
        ///     Extracts and returns the controller of this routedata
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetController(this RouteData @this)
        {
            return @this.Values["controller"] as string;
        }

        /// <summary>
        ///     Sets the controller in this routedata
        /// </summary>
        /// <param name="this"></param>
        /// <param name="controller"></param>
        public static void SetController(this RouteData @this, Controller controller)
        {
            @this.SetController(controller.GetType());
        }

        /// <summary>
        ///     Sets the controller in this routedata
        /// </summary>
        /// <param name="this"></param>
        /// <param name="controllerType"></param>
        public static void SetController(this RouteData @this, Type controllerType)
        {
            @this.SetController(controllerType.Name.Replace("Controller", string.Empty));
        }

        /// <summary>
        ///     Sets the controller in this routedata
        /// </summary>
        /// <param name="this"></param>
        /// <param name="controller"></param>
        public static void SetController(this RouteData @this, [AspMvcController] string controller)
        {
            @this.Values["controller"] = controller;
        }

        /// <summary>
        ///     Extracts and returns the action of this routedata
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string GetAction(this RouteData @this)
        {
            return @this.Values["action"] as string;
        }

        /// <summary>
        ///     Sets the action in this routedata
        /// </summary>
        /// <param name="this"></param>
        /// <param name="action"></param>
        public static void SetAction(this RouteData @this, [AspMvcAction] string action)
        {
            @this.Values["action"] = action;
        }

        /// <summary>
        ///     Sets the controller and action in the route data
        /// </summary>
        /// <param name="this"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        public static void SetControllerAndAction(this RouteData @this, [AspMvcController] string controller, [AspMvcAction] string action)
        {
            @this.SetController(controller);
            @this.SetAction(action);
        }
    }
}
