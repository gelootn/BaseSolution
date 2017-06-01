using System.Collections.Generic;

namespace BaselineSolution.Framework.Infrastructure.Contracts
{
    /// <summary>
    ///     Indicates that this class implements a tree-like structure,
    ///     where instances of this class can contain an optional parent and optional children. 
    /// </summary>
    /// <typeparam name="TClass"></typeparam>
    public interface ITreeHierarchy<TClass> where TClass : class, IIdentifiable
    {
        /// <summary>
        ///     Gets or sets the parent id
        /// </summary>
        int? ParentId { get; set; }

        /// <summary>
        ///     Gets or sets the parent
        /// </summary>
        TClass Parent { get; set; }

        /// <summary>
        ///     Gets or sets the children
        /// </summary>
        ICollection<TClass> Children { get; set; }
    }
}
