﻿using System.Reflection;
using Autofac;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Repositories;
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

            base.Load(builder);
        }
    }
}
