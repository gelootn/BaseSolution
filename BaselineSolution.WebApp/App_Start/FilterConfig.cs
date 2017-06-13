using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaselineSolution.WebApp.Infrastructure.Filters;

namespace BaselineSolution.WebApp.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomAuthorizeAttribute(FilterScope.Global, "Home", "Authentication", "Login"));
            //filters.Add(new LogExceptionFilterAttribute());
        }
    }
}