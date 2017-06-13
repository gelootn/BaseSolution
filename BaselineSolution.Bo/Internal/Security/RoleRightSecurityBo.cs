using System;

namespace BaselineSolution.Bo.Internal.Security
{
    [Serializable]
    public class RoleRightSecurityBo : BaseBo
    {

        public RoleSecurityBo Role { get; set; }
        public RightSecurityBo Right { get; set; }

        public bool? Allow { get; set; }
        public int RightId { get; set; }
    }
}
