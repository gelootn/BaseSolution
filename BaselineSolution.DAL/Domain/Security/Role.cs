using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
        /// Returns true if this role is allowed to access the given right
        /// </summary>
        /// <param name="right"></param>
        /// <returns>true if this role is allowed to access the given right or false otherwise</returns>
        public bool HasRight(Right right)
        {
            Right tempRight = right;
            bool? hasRight = ValidateWithRoleRights(tempRight);
            if (hasRight != null)
            {
                return hasRight.Value;
            }
            while (tempRight.Parent != null)
            {
                tempRight = tempRight.Parent;
                hasRight = ValidateWithRoleRights(tempRight);
                if (hasRight != null)
                {
                    return hasRight.Value;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if this role has a RoleRight and that RoleRight's 'Allow' property is set to true.
        /// </summary>
        /// <param name="right"></param>
        /// <returns>True if this role has a RoleRight and that RoleRight's 'Allow' property is set to true.</returns>
        private bool? ValidateWithRoleRights(Right right)
        {
            RoleRight roleRight = RoleRights.SingleOrDefault(r => r.RightId == right.Id);
            return roleRight != null ? roleRight.Allow : null;
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
