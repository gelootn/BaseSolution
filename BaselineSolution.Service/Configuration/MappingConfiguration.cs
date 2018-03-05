using System;
using AutoMapper;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;

namespace BaselineSolution.Service.Configuration
{
    public static class MappingConfiguration
    {
        public static void ConfigureMapper()
        {
            Action<IMapperConfigurationExpression> config = cfg =>
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
            };
            Mapper.Initialize(config);
        }
    }
}
