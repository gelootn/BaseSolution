using System.Reflection;
using Autofac;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Domain.Shared;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.DAL.UnitOfWork.Implementations.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Service.Internal;
using Module = Autofac.Module;

namespace BaselineSolution.IOC
{
    public class MvcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new DatabaseContext()).As<DatabaseContext>().InstancePerRequest();


            var dal = Assembly.GetAssembly(typeof(BaselineSolution.DAL.Repositories.GenericRepository<>));
            var service = Assembly.GetAssembly(typeof(BaselineSolution.Service.Internal.Service));


            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerRequest();


            builder.RegisterAssemblyTypes(dal)
                .Where(t => t.Name.EndsWith("UnitOfWork"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(service)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            /*builder.RegisterType<SecurityService>().As<ISecurityService>().InstancePerRequest();

            builder.RegisterType<SecurityUnitOfWork>().As<ISecurityUnitOfWork>().InstancePerRequest();*/

            /*builder.RegisterType<GenericRepository<User>>().As<IGenericRepository<User>>().InstancePerRequest();
            builder.RegisterType<GenericRepository<Right>>().As<IGenericRepository<Right>>().InstancePerRequest();
            builder.RegisterType<GenericRepository<SystemLanguage>>().As<IGenericRepository<SystemLanguage>>().InstancePerRequest();
            builder.RegisterType<GenericRepository<Account>>().As<IGenericRepository<Account>>().InstancePerRequest();
            builder.RegisterType<GenericRepository<Role>>().As<IGenericRepository<Role>>().InstancePerRequest();*/



            base.Load(builder);
        }
    }
}
