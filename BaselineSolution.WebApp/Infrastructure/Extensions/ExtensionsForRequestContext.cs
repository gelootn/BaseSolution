using System.Web;
using System.Web.Routing;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Infrastructure.Models.Authentication;

namespace BaselineSolution.WebApp.Infrastructure.Extensions
{
    public static class ExtensionsForRequestContext
    {
        public static bool Authenticate(this RequestContext requestContext)
        {
            return new Authenticator(requestContext).Authenticate();
        }

        public static bool Authenticate(this RequestContext requestContext, string rightKey)
        {
            return new Authenticator(requestContext).Authenticate(rightKey);
        }

        public static bool Authenticate(this RequestContext requestContext, [AspMvcArea] string areaName, [AspMvcController] string controllerName, [AspMvcAction] string actionName)
        {
            return new Authenticator(requestContext).Authenticate(areaName, controllerName, actionName);
        }

        public static string GetUsername(this RequestContext requestContext)
        {
            return requestContext.HttpContext.User != null ? requestContext.HttpContext.User.Identity.Name : HttpContext.Current.User.Identity.Name;
        }
    }
}