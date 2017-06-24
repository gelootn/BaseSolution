using System.Net;
using System.Web.Mvc;

namespace BaselineSolution.WebApp.Components.Models.Authentication
{
    public class UnauthorizedJsonResult : JsonResult
    {
        public UnauthorizedJsonResult(string returnUrl)
        {
            Data = new { returnUrl };
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            base.ExecuteResult(context);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.HttpContext.Response.End();
        }
    }
}