using System.Collections.Generic;
using BaselineSolution.Framework.Infrastructure;

namespace BaselineSolution.Framework.ApiHandling
{
    /// <summary>
    /// Base class for API view models
    /// </summary>
    public abstract class ApiViewModel<TBo>  where TBo: BaseBo 
    {
        /// <summary>
        /// The object
        /// </summary>
        public TBo Result { get; set; }

        /// <summary>
        /// Collection of links for the view model
        /// </summary>
        public ICollection<HyperMediaLink> Links { get; set; }
    }
}