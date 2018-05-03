using System.Web.Http;
using System.Web.Http.Routing;
using BaselineSolution.WebApp.Infrastructure.Filters;
using Microsoft.Web.Http.Routing;

namespace BaselineSolution.WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof(ApiVersionRouteConstraint)
                }
            };
            config.MapHttpAttributeRoutes( constraintResolver );
            config.AddApiVersioning();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ApiAuthorizeFilter());
        }
    }
}
