using System;
using AutoMapper;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.Domain.Shared;

namespace BaselineSolution.Service.Configuration
{
    public static class MappingConfiguration
    {
        public static void ConfigureMapper()
        {
            Action<IMapperConfigurationExpression> config = cfg =>
            {
                SecurityMgntMapping(cfg);
                SharedMapping(cfg);
            };
            Mapper.Initialize(config);
        }

        private static void SharedMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<SystemLanguageBo, SystemLanguage>();
        }

        private static void SecurityMgntMapping(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<UserBo, User>();
            cfg.CreateMap<DisplayObject, User>()
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Display));

            cfg.CreateMap<AccountBo, Account>();
            cfg.CreateMap<Account, DisplayObject>()
                .ForMember(x => x.Display, src => src.MapFrom(x => x.Name));

            cfg.CreateMap<RoleBo, Role>();
            cfg.CreateMap<DisplayObject, Role>()
                .ForMember(x => x.Name, src => src.MapFrom(x => x.Display));
        }
    }
}
