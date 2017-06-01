using System;

namespace BaselineSolution.Framework.Infrastructure.Contracts
{
    /// <summary>
    ///     Contains change tracking properties
    /// </summary>
    public interface IChangeTrackable
    {
        /// <summary>
        ///     Gets or sets the creation user id.
        /// </summary>
        int? CreationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the creation date.
        /// </summary>
        DateTime? CreationDate { get; set; }

        /// <summary>
        ///     Gets or sets the modification user id.
        /// </summary>
        int? ModificationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the modification date.
        /// </summary>
        DateTime? ModificationDate { get; set; }
    }
}
