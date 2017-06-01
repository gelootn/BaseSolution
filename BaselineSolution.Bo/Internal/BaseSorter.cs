using System.Collections.Generic;

namespace BaselineSolution.Bo.Internal
{
    public abstract class BaseSorter
    {
        public int PageSize { get; set; }
        public int Page { get; set; }

        public IEnumerable<CollumnSorter> Sorters { get; set; }

    }
}
