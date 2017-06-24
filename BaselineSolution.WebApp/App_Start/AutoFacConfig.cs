using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using BaselineSolution.IOC;

namespace BaselineSolution.WebApp
{
    public class AutoFacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule<MvcModule>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }

}