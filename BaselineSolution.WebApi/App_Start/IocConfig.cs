using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using BaselineSolution.Framework.Logging;
using BaselineSolution.IOC;
using BaselineSolution.WebApi.Infrastructure.Controllers;

namespace BaselineSolution.WebApi.App_Start
{
    public class IocConfig
    {
        public static void ConfigureContainer(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule<BackendModule>();
            builder.RegisterModule(new LogModule(NlogConfig.GetConfig("Baseline.WebApi"), "Baseline.WebApi"));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("FilterHandler"))
                .AsImplementedInterfaces().InstancePerRequest();


            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}