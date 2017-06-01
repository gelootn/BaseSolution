using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.DAL.Infrastructure.Bases
{
    /// <summary>
    ///     This is the base class for all entities (classes that need to be persisted to the database)
    /// </summary>
    public abstract class Entity : IIdentifiable, IDeletable, IChangeTrackable
    {
        /// <summary>
        ///     Gets or sets the id.
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        ///     Gets or sets the creation user id.
        /// </summary>
        public virtual int? CreationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the creation date.
        /// </summary>
        public virtual DateTime? CreationDate { get; set; }

        /// <summary>
        ///     Gets or sets the modification user id.
        /// </summary>
        public virtual int? ModificationUserId { get; set; }

        /// <summary>
        ///     Gets or sets the modification date.
        /// </summary>
        public virtual DateTime? ModificationDate { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether deleted.
        /// </summary>
        public bool Deleted { get; set; }


    }
}
