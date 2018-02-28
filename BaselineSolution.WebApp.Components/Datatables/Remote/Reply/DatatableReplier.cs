using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.WebApp.Components.Datatables.Remote.Processors;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Reply
{
    /// <summary>
    /// Helper class that can parse a <see cref="DatatableReply"/> and give a <see cref="DatatableReply"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DatatableReplier<TEntity> where TEntity : class
    {
        private RemoteDatatable<TEntity> Datatable { get; set; }
        private IDatatableProcessor<TEntity> DatatableProcessor { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="DatatableReplier{TEntity}"/>
        /// </summary>
        /// <param name="remoteDatatable">The datatable for which this request was made</param>
        /// <param name="datatableProcessor">The datatable processor that will provide the data per request</param>
        public DatatableReplier(RemoteDatatable<TEntity> remoteDatatable, IDatatableProcessor<TEntity> datatableProcessor)
        {
            Datatable = remoteDatatable;
            DatatableProcessor = datatableProcessor;
        }

        private DatatableReply ReplyInternal(DatatableRequest request, List<TEntity> entities, int filteredCount, int totalCount)
        {
            var data = new IDictionary<string, string>[entities.Count];
            for (int i = 0; i < entities.Count; i++)
            {
                var entity = entities[i];
                var dataForEntity = new Dictionary<string, string>();
                foreach (var column in Datatable.Columns)
                {
                    dataForEntity[column.Name] = column.DisplayFunction(entity);
                }
                data[i] = dataForEntity;
            }

            return new DatatableReply
            {
                Echo = Convert.ToInt32(request.Echo),
                Columns = string.Join(",", Datatable.Columns.Select(c => c.Name)),
                Data = data,
                TotalDisplayRecords = filteredCount,
                TotalRecords = totalCount
            };
        }

        /// <summary>
        /// Returns an instance of <see cref="DatatableReply"/> in response to the <paramref name="request"/>
        /// </summary>
        /// <param name="request">The request to respond to</param>
        /// <returns>an instance of <see cref="DatatableReply"/> in response to the <paramref name="request"/></returns>
        public DatatableReply Reply(DatatableRequest request)
        {
            int totalCount, filteredCount;
            var entities = DatatableProcessor.Process(Datatable, request, out filteredCount, out totalCount);
            return ReplyInternal(request, entities, filteredCount, totalCount);
        }
    }
}
