using System.Web.Http;
using BaselineSolution.WebApi.App_Start;

namespace BaselineSolution.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IocConfig.ConfigureContainer(GlobalConfiguration.Configuration);
        }
    }
}
