using BaselineSolution.WebApi.Infrastructure.Filters;

namespace BaselineSolution.WebApi.Filters.Account
{
    /// <summary>
    /// Filter for the account List
    /// </summary>
    public class AccountBoFilter : BoFilter
    {
        /// <summary>
        /// Filter the account name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Filter the account description
        /// </summary>
        public string Description { get; set; }

    }
}