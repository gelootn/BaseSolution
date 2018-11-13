namespace BaselineSolution.WebApi.Infrastructure.Filters
{
    /// <summary>
    /// filter object containing the paging settings
    /// </summary>
    public abstract class BoFilter
    {
        /// <summary>
        /// The size of the page
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// The page number
        /// </summary>
        public int Page { get; set; }
    }
}