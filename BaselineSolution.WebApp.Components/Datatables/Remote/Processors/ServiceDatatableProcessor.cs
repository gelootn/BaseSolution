using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Infrastructure.Contracts;
using BaselineSolution.Framework.Services;
using BaselineSolution.WebApp.Components.Datatables.Remote.Filtering;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;
using BaselineSolution.WebApp.Components.Datatables.Remote.Sorting;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Processors
{
    public class ServiceDatatableProcessor<TEntity> : IDatatableProcessor<TEntity> where TEntity : class, IIdentifiable
    {
        private IListService<TEntity> Service { get; set; }
        private IEntityFilter<TEntity> BaseFilter { get; set; }
        private IEntitySorter<TEntity> BaseSorter { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="service">The repository from which the data can be fetched</param>
        /// <param name="baseFilter">The base filter that needs to be applied to the data before the request is parsed</param>
        /// <param name="baseSorter">The base sorter that needs to be applied to the data before the request is parsed</param>
        public ServiceDatatableProcessor(
            IListService<TEntity> service, 
            IEntityFilter<TEntity> baseFilter = null,
            IEntitySorter<TEntity> baseSorter = null)
        {
            Service = service;
            BaseFilter = baseFilter;
            BaseSorter = baseSorter;

        }

        public List<TEntity> Process(RemoteDatatable<TEntity> remoteDatatable, DatatableRequest datatableRequest, out int filteredCount, out int totalCount)
        {
            var filter = new DatatableFilter<TEntity>(BaseFilter, remoteDatatable, datatableRequest);
            var sorter = new DatatableSorter<TEntity>(BaseSorter, remoteDatatable, datatableRequest);
            var page = datatableRequest.DisplayStart / datatableRequest.DisplayLength;
            var pageSize = datatableRequest.DisplayLength;
            totalCount = Service.Count(BaseFilter);
            filteredCount = datatableRequest.ContainsFiltering || BaseFilter != null ? Service.Count(filter) : totalCount;
            return Service.List(sorter, filter, page, pageSize).ToList();
        }
    }


}
