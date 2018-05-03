using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;
using BaselineSolution.Facade.Internal;
using BaselineSolution.WebApp.Infrastructure.Constants;

namespace BaselineSolution.WebApp.Infrastructure.Filters
{
    public class ApiAuthorizeFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // if the controller or the method is decorated with [AllowAnonymous], we don't perform authentication
            if (actionContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes<System.Web.Http.AllowAnonymousAttribute>(true).Any()
                || actionContext.ActionDescriptor.GetCustomAttributes<System.Web.Http.AllowAnonymousAttribute>(true).Any())
            {
                return;
            }

            var autherize = actionContext.Request.Headers.Authorization;
            if (autherize != null && autherize.Scheme == "Basic")
            {
                var encoding = Encoding.GetEncoding("iso-8859-1");

                var userAndPass = encoding.GetString(Convert.FromBase64String(autherize.Parameter));
                var user = userAndPass.Split(':')[0];
                var pass = userAndPass.Split(':')[1];
                var security = DependencyResolver.Current.GetService<ISecurityService>();

                var userResponse = security.Login(user, pass, out var userBo);
                if (!userResponse.IsSuccess)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
                else
                {
                    if (userBo.IsAdministrator())
                        return;
                    
                    var rightResponse = security.CheckUserRight(userBo, GetKey(actionContext));
                    if (rightResponse.Value)
                    {
                        return;
                    }
                }
            }

            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            base.OnAuthorization(actionContext);
        }

        private string GetKey(HttpActionContext actionContext)
        {
            var area = StringConstants.ApiArea;
            var controller = actionContext.ControllerContext.ControllerDescriptor.ControllerType.Name;
            var action = actionContext.ActionDescriptor.ActionName;
            return string.Format("{0}.{1}.{2}", area, controller, action);
        }
    }
}