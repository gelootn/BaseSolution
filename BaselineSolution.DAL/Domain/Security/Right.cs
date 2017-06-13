using System.Collections.Generic;
using System.Collections.ObjectModel;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.DAL.Domain.Security
{
    public class Right : Entity, ITreeHierarchy<Right>
    {
        private ICollection<Right> _children;

        /// <summary>
        /// Gets or Sets the ParentId
        /// </summary>
        public virtual int? ParentId { get; set; }

        /// <summary>
        /// Gets or Sets the Parent
        /// </summary>
        public virtual Right Parent { get; set; }

        /// <summary>
        /// Gets or Sets the Children
        /// </summary>
        public virtual ICollection<Right> Children
        {
            get
            {
                return _children ?? (_children = new Collection<Right>());
            }
            set
            {
                _children = value;
            }
        }

        /// <summary>
        /// Gets or Sets the Key
        /// </summary>
        public virtual string Key { get; set; }
    }
}
