﻿using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.DAL.Repositories;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Extensions;
using BaselineSolution.Framework.Infrastructure;
using BaselineSolution.Framework.Infrastructure.Attributes;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Infrastructure.Filtering;
using BaselineSolution.Framework.Infrastructure.Sorting;
using BaselineSolution.Framework.Logging;
using BaselineSolution.Framework.Response;
using BaselineSolution.Service.Infrastructure.Extentions;
using BaselineSolution.Service.Translators.Internal;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BaselineSolution.Service.Infrastructure.Internal
{
    public class GenericService<TBo, TEntity> : BaseService, IGenericService<TBo>
        where TBo : BaseBo, new()
        where TEntity : Entity, new()
    {

        private readonly IGenericRepository<TEntity> _repository;
        private readonly ITranslator<TBo, TEntity> _translator;

        public GenericService(IGenericRepository<TEntity> repository, ITranslator<TBo, TEntity> translator, ILogging log) : base(log)
        {
            _repository = repository;
            _translator = translator;
        }

        Response<TBo> IGenericService<TBo>.GetById(int id)
        {
            return GetByIdAsyncInternal(id).Result;
            ;
        }

        async Task<Response<TBo>> IGenericService<TBo>.GetByIdAsync(int id)
        {
            return await GetByIdAsyncInternal(id);
        }

        Response<int> IGenericService<TBo>.AddOrUpdate(TBo bo, int userId)
        {
            try
            {
                if (!bo.IsValid())
                    return new Response<int>().AddValidationMessage(bo.ValidationMessages);

                var item = bo.IsNew ? new TEntity() : _repository.FindById(bo.Id);

                if (item == null)
                    return new Response<int>().AddItemNotFound(bo.Id);

                item = bo.UpdateModel(item, _translator);
                _repository.AddOrUpdate(item);
                _repository.Commit(userId);

                return new Response<int>(item.Id);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<int>().AddErrorMessage(e);
            }

        }

        Response<bool> IGenericService<TBo>.Delete(int id, int userId)
        {
            try
            {
                var item = _repository.FindById(id);
                if (item == null)
                    return new Response<bool>().AddItemNotFound(id);

                _repository.Delete(id);
                _repository.Commit(userId);


                return new Response<bool>(true);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<bool>().AddErrorMessage(e);
            }

        }

        Response<int> IGenericService<TBo>.Count(IEntityFilter<TBo> filter)
        {
            return CountAsyncInternal(filter).Result;
        }

        async Task<Response<int>> IGenericService<TBo>.CountAsync(IEntityFilter<TBo> filter)
        {
            return await CountAsyncInternal(filter);
        }

        Response<TBo> IGenericService<TBo>.List(IEntityFilter<TBo> filter)
        {
            return ListAsyncInternal(filter, null, null, null).Result;
        }

        async Task<Response<TBo>> IGenericService<TBo>.ListAsync(IEntityFilter<TBo> filter)
        {
            return await ListAsyncInternal(filter, null, null, null);
        }

        Response<TBo> IGenericService<TBo>.List(IEntityFilter<TBo> filter, [CanBeNull] IEntitySorter<TBo> sorter, [CanBeNull] int? page, [CanBeNull] int? pageSize)
        {
            return ListAsyncInternal(filter, sorter, page, pageSize).Result;
        }

        async Task<Response<TBo>> IGenericService<TBo>.ListAsync(IEntityFilter<TBo> filter, [CanBeNull] IEntitySorter<TBo> sorter, [CanBeNull] int? page, [CanBeNull] int? pageSize)
        {
            return await ListAsyncInternal(filter, sorter, page, pageSize);
        }

        private async Task<Response<TBo>> ListAsyncInternal(IEntityFilter<TBo> filter, IEntitySorter<TBo> sorter, int? page, int? pageSize)
        {
            try
            {
                var list = _repository.List();
                list = list.Filter(filter);
                if (sorter != null && page.HasValue && pageSize.HasValue)
                {
                    list = list.Sort(sorter);
                    list = list.Skip((page.Value) * pageSize.Value);
                    list = list.Take(pageSize.Value);
                }

                var filteredList = await list.ToListAsync();
                var translated = filteredList.Select(x => x.ToBo(_translator)).ToList();

                return new Response<TBo>(translated);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<TBo>().AddErrorMessage(e);
            }
        }

        private async Task<Response<TBo>> GetByIdAsyncInternal(int id)
        {
            try
            {
                TEntity item = await _repository.FindByIdAsync(id);
                if (item == null)
                    return new Response<TBo>().AddItemNotFound(id);

                return new Response<TBo>(item.ToBo(_translator));
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<TBo>().AddErrorMessage(e);
            }
        }

        private async Task<Response<int>> CountAsyncInternal(IEntityFilter<TBo> filter)
        {
            try
            {
                var list = _repository.List();
                list = list.Filter(filter);
                var result = await list.CountAsync();
                return new Response<int>(result);
            }
            catch (Exception e)
            {
                Log.Error(e);
                return new Response<int>().AddErrorMessage(e);
            }
        }
    }
}
