using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Services;
using BaselineSolution.Service.Internal;

namespace BaselineSolution.Service.Security
{
    public class UserListService : IListService<UserBo>
    {
        private readonly ISecurityUnitOfWork _securityUnitOfWork;

        public UserListService(ISecurityUnitOfWork securityUnitOfWork)
        {
            _securityUnitOfWork = securityUnitOfWork;
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<UserBo, User>();
                cfg.CreateMap<User, UserBo>();
            });

        }

        int IListService<UserBo>.Count(IEntityFilter<UserBo> baseFilter)
        {
            var filter = baseFilter.GetRepoFilter<UserBo, User>();
            return _securityUnitOfWork.UserRepo.Count(filter);
        }

        IEnumerable<UserBo> IListService<UserBo>.List(IEntitySorter<UserBo> sorter, IEntityFilter<UserBo> filters, int page, int pageSize)
        {
            var query = _securityUnitOfWork.UserRepo.List();

            query = query.Filter(filters);
            query = query.Sort(sorter);
            query = query.Skip((page - 1) * pageSize);
            query = query.Take(pageSize);

            //replace next line by a correct mapper
            return Mapper.Map<IEnumerable<User>, IEnumerable<UserBo>>(query.ToList());
        }
    }    
}
