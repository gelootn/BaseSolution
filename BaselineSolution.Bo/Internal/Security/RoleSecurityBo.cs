using System;
using System.Collections.Generic;
using System.Linq;
using BaselineSolution.Framework.Infrastructure;
using BaselineSolution.Framework.Infrastructure.Contracts;
using FluentValidation;

namespace BaselineSolution.Bo.Internal.Security
{
    [Serializable]
    public class RoleSecurityBo : BaseBo, ITreeHierarchy<RoleSecurityBo>
    {
        private ICollection<RoleSecurityBo> _children;
        private ICollection<RoleRightSecurityBo> _roleRights;
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public RoleSecurityBo Parent { get; set; }

        public ICollection<RoleSecurityBo> Children
        {
            get { return _children ?? (_children = new List<RoleSecurityBo>()); }
            set { _children = value; }
        }

        public ICollection<RoleRightSecurityBo> RoleRights
        {
            get { return _roleRights ?? (_roleRights = new List<RoleRightSecurityBo>()); }
            set { _roleRights = value; }
        }

        public bool HasRight(RightSecurityBo right)
        {
            RightSecurityBo tempRight = right;
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

        private bool? ValidateWithRoleRights(RightSecurityBo right)
        {
            RoleRightSecurityBo roleRight = RoleRights.SingleOrDefault(r => r.RightId == right.Id);
            return roleRight != null ? roleRight.Allow : null;
        }
    }
}
