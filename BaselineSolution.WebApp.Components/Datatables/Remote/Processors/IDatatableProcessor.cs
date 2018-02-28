using System.Collections.Generic;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Processors
{
    public interface IDatatableProcessor<TEntity> where TEntity : class
    {
        List<TEntity> Process(RemoteDatatable<TEntity> remoteDatatable, DatatableRequest datatableRequest, out int filteredCount, out int totalCount);
    }
}
