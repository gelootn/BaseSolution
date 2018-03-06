using System.Collections.Generic;
using BaselineSolution.Bo.Models.Security;

namespace BaselineSolution.WebApp.Areas.Security.ViewModels.Role
{
    public class RoleRightsViewModel
    {
        public RoleFullBo RoleBo { get; set; }
        public UserBo User { get; set; }
        public List<RestrictedRightBo> AllowedRights { get; set; }
    }
}