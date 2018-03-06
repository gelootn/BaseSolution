using System.Collections.Generic;

namespace BaselineSolution.Bo.Models.Security
{
    public class RoleFullBo : RoleBo
    {
        public ICollection<RoleRightBo> RoleRights { get; set; }
    }
}
