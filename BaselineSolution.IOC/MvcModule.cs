using System.Reflection;
using Autofac;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;
using BaselineSolution.Service.Configuration;
using BaselineSolution.Service.Internal;
using BaselineSolution.Service.Security;
using Module = Autofac.Module;

namespace BaselineSolution.IOC
{
    public class MvcModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            MappingConfiguration.ConfigureMapper();

            builder.Register(c => new DatabaseContext()).As<DatabaseContext>().InstancePerRequest();


            var dal = Assembly.GetAssembly(typeof(BaselineSolution.DAL.Repositories.GenericRepository<>));
            var service = Assembly.GetAssembly(typeof(BaselineSolution.Service.Internal.Service));


            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerRequest();
            //builder.RegisterGeneric(typeof(GenericService<,>)).As(typeof(IGenericService<>)).InstancePerRequest();

            builder.RegisterType<GenericService<UserBo, User>>().As<IGenericService<UserBo>>();
            builder.RegisterType<GenericService<AccountBo, Account>>().As<IGenericService<AccountBo>>();
            builder.RegisterType<GenericService<RoleBo, Role>>().As<IGenericService<RoleBo>>();

            builder.RegisterType<SecurityService>().As<ISecurityService>();
            builder.RegisterType<SecurityMgntService>().As<ISecurityMgntService>();


            builder.RegisterAssemblyTypes(dal)
                .Where(t => t.Name.EndsWith("UnitOfWork"))
                .AsImplementedInterfaces().InstancePerRequest();

            /*builder.RegisterAssemblyTypes(service)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();
                */
            builder.RegisterAssemblyTypes(service)
                .Where(t => t.Name.EndsWith("Translator"))
                .AsImplementedInterfaces().InstancePerRequest();

            base.Load(builder);
        }
    }
}
