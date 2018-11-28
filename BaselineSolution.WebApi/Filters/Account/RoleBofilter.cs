using BaselineSolution.WebApi.Infrastructure.Filters;

namespace BaselineSolution.WebApi.Filters.Account
{
    /// <summary>
    /// Filter for the role list
    /// </summary>
    public class RoleBoFilter : BoFilter
    {
        /// <summary>
        /// Name property filter
        /// </summary>
        public string Name { get; set; }
    }
}