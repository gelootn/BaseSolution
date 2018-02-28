using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.XpressionMapper.Extensions;
using BaselineSolution.Bo.Models.Security;
using BaselineSolution.DAL.Domain.Security;
using BaselineSolution.DAL.UnitOfWork.Interfaces.Security;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Services;
using LinqKit;

namespace BaselineSolution.Service.Security
{
    public class UserListService : IListService<UserBo>
    {
        private readonly ISecurityUnitOfWork _securityUnitOfWork;

        public UserListService(ISecurityUnitOfWork securityUnitOfWork)
        {
            _securityUnitOfWork = securityUnitOfWork;
        }

        int IListService<UserBo>.Count(IEntityFilter<UserBo> baseFilter)
        {
            Expression<Func<User, bool>> repofilter = user => true;
            if (baseFilter != null)
            {
                foreach (var filter in baseFilter.Predicates)
                {
                    Expression<Func<User,bool>> exp = Mapper.Instance.MapExpression<Expression<Func<UserBo,bool>>, Expression<Func<User,bool>>>(filter);
                    repofilter = repofilter.And(exp);
                }
            }

            return _securityUnitOfWork.UserRepo.Count(repofilter);
        }

        IEnumerable<UserBo> IListService<UserBo>.List(IEntitySorter<UserBo> sorter, IEntityFilter<UserBo> filters, int page, int pageSize)
        {
            Expression<Func<User, bool>> repofilter = user => true;
            if (filters != null)
            {
                foreach (var filter in filters.Predicates)
                {
                    Expression<Func<User,bool>> exp = Mapper.Instance.MapExpression<Expression<Func<UserBo,bool>>, Expression<Func<User,bool>>>(filter);
                    repofilter = repofilter.And(exp);
                }
            }
            //Func<User, bool> filter = user => true;
            

            var result = _securityUnitOfWork.UserRepo.List(repofilter, page, pageSize, out int total);

            return Mapper.Map<IEnumerable<User>, IEnumerable<UserBo>>(result.ToList());
        }
    }
}
