using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.WebApp.Components.Datatables.Remote.Request;

namespace BaselineSolution.WebApp.Components.Datatables.Remote.Processors
{
    public class ListServiceDatatableProcessor<TEntity> : IDatatableProcessor<TEntity> where TEntity : class
    {
        public List<TEntity> Process(RemoteDatatable<TEntity> remoteDatatable, DatatableRequest datatableRequest, out int filteredCount,
            out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
