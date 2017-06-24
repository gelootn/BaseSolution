using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.WebApp.Infrastructure.Extensions;
using BaselineSolution.WebApp.Infrastructure.Models.Authentication;

namespace BaselineSolution.WebApp.Infrastructure.Filters
{
    public class CustomAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly string _action;
        private readonly string _area;
        private readonly string _controller;

        private readonly string[] _allowedPatterns = new[] { "errorlog" };

        public CustomAuthorizeAttribute(FilterScope scope, [AspMvcArea] string area, [AspMvcController] string controller, [AspMvcAction] string action)
        {
            Order = (int)scope;
            _area = area;
            _controller = controller;
            _action = action;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // if the controller or the method is decorated with [AllowAnonymous], we don't perform authentication
            if (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any()
                || filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true).Any())
            {
                return;
            }

            // If the request is authenticated (the user is logged in) 
            // and the controller or method is decorated with [AllowAuthenticated], we don't perform authentication
            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated
                && (filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAuthenticatedAttribute), true).Any()
                || filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAuthenticatedAttribute), true).Any()))
            {
                return;
            }

            // If the request is not inside an area, then this is shared functionality that cannot be protected by the rights system
            var area = filterContext.RouteData.GetArea();
            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated && string.IsNullOrEmpty(area))
                return;

            string requestUrl = HttpContext.Current.Request.RawUrl;

            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated
                && _allowedPatterns.Any(pattern => requestUrl.Replace("/", "").StartsWith(pattern, StringComparison.OrdinalIgnoreCase)))
                return;

            var urlHelper = new UrlHelper(filterContext.RequestContext);

            var returnActionWithReturnUrl = urlHelper.Action(_action, _controller, new { area = _area, ReturnUrl = requestUrl });
            var returnActionWithoutReturnUrl = urlHelper.Action(_action, _controller, new { area = _area });

            // Ajax requests get special treatment: unauthorized result is in JSON format
            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var ajaxLoginWithoutReturnResult = new UnauthorizedJsonResult(returnActionWithoutReturnUrl);
                // ajax requests never have a return url
                AuthenticateFilterContext(filterContext, ajaxLoginWithoutReturnResult, ajaxLoginWithoutReturnResult);
            }
            else
            {
                var defaultLoginWithReturnUrlResult = new RedirectResult(returnActionWithReturnUrl);
                var defaultLoginWithoutReturnUrlResult = new RedirectResult(returnActionWithoutReturnUrl);
                AuthenticateFilterContext(filterContext, defaultLoginWithReturnUrlResult, defaultLoginWithoutReturnUrlResult);
            }
        }

        private void AuthenticateFilterContext(AuthorizationContext filterContext, ActionResult notAuthenticatedResult, ActionResult rightAuthenticationFailedResult)
        {
            // Catch unauthenticated requests on controllers that don't allow anonymous requests
            if (!filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = notAuthenticatedResult;
            }

            // Catch authenticated requests and check if the user has access to this action
            else
            {
                if (!filterContext.RequestContext.Authenticate())
                {
                    // failed authentication returns to login page without return url to avoid loops
                    filterContext.Result = rightAuthenticationFailedResult;
                }
            }
        }
    }
}