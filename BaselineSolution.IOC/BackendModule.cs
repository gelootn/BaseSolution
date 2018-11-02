using System.Reflection;
using Autofac;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Domain.Shared;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Security;
using BaselineSolution.Facade.Shared;
using BaselineSolution.Service.Configuration;
using BaselineSolution.Service.Infrastructure.Internal;
using BaselineSolution.Service.Security;
using BaselineSolution.Service.Shared;
using Module = Autofac.Module;

namespace BaselineSolution.IOC
{
    public class BackendModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            MappingConfiguration.ConfigureMapper();

            builder.Register(c => new DatabaseContext()).As<DatabaseContext>().InstancePerRequest();

            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(GenericService<,>)).As(typeof(IGenericService<>)).InstancePerRequest();

            var dal = Assembly.GetAssembly(typeof(GenericRepository<>));
            var service = Assembly.GetAssembly(typeof(BaseService));

            builder.RegisterType<GenericService<UserBo, User>>().As<IGenericService<UserBo>>();
            builder.RegisterType<GenericService<AccountBo, Account>>().As<IGenericService<AccountBo>>();
            builder.RegisterType<GenericService<RoleBo, Role>>().As<IGenericService<RoleBo>>();
            builder.RegisterType<GenericService<RightBo, Right>>().As<IGenericService<RightBo>>();
            builder.RegisterType<GenericService<SystemLanguageBo, SystemLanguage>>().As<IGenericService<SystemLanguageBo>>();

            builder.RegisterType<SecurityService>().As<ISecurityService>();
            builder.RegisterType<SecurityMgntService>().As<ISecurityMgntService>();
            builder.RegisterType<SharedService>().As<ISharedService>();
            


            builder.RegisterAssemblyTypes(dal)
                .Where(t => t.Name.EndsWith("UnitOfWork"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterAssemblyTypes(service)
                .Where(t => t.Name.EndsWith("Translator"))
                .AsImplementedInterfaces().InstancePerRequest();

            base.Load(builder);
        }
    }
}
