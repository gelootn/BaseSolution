using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using BaselineSolution.DAL.Infrastructure.Bases;
using BaselineSolution.Framework.Infrastructure.Contracts;

namespace BaselineSolution.DAL.Domain.Security
{
    /// <summary>
    ///     A role represents a group of rights and has a name
    /// </summary>
    [DebuggerDisplay("Id = {Id}, Name = {Name}, ParentId = {ParentId}")]
    public class Role : Entity, ITreeHierarchy<Role>
    {
        /// <summary>
        ///     The _role rights.
        /// </summary>
        private ICollection<RoleRight> _roleRights;

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Gets or sets the role rights.
        /// </summary>
        public virtual ICollection<RoleRight> RoleRights
        {
            get { return _roleRights ?? (_roleRights = new List<RoleRight>()); }
            set { _roleRights = new List<RoleRight>(value); }
        }

        public virtual int? ParentId { get; set; }

        public virtual Role Parent { get; set; }

        private ICollection<Role> _children;
        public virtual ICollection<Role> Children
        {
            get { return _children ?? (_children = new Collection<Role>()); }
            set { _children = value; }
        }



        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0}, Name: {1}, ParentId: {2}", base.ToString(), Name, ParentId);
        }
    }
}
