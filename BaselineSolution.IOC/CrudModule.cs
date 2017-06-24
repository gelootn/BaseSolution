using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Database;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Service.Internal;
using BaselineSolution.Service.Translators.Internal;
using BaselineSolution.Service.Translators.Security;
using Module = Autofac.Module;

namespace BaselineSolution.IOC
{
    public  class CrudModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            
            RegisterCrudService(builder);
            RegisterCrudTranslator(builder);

            base.Load(builder);
        }

        private void RegisterCrudService(ContainerBuilder builder)
        {
            builder.RegisterType(typeof(CrudService<UserBo, UserCommitBo, User>)).As(typeof(ICrudService<UserBo, UserCommitBo>));

        }

        private void RegisterCrudTranslator(ContainerBuilder builder)
        {
            builder.RegisterType<UserCrudTranslator>().As<ICrudTranslator<UserBo, UserCommitBo, User>>();
        }
    }
}
