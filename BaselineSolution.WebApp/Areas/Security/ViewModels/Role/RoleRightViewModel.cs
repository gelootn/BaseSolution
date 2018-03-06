using System.Linq;
using BaselineSolution.Bo.Models.Security;

namespace BaselineSolution.WebApp.Areas.Security.ViewModels.Role
{
    public class RoleRightViewModel
    {
        public RestrictedRightBo Right { get; set; }
        public RoleFullBo Role { get; set; }
        public bool? Allow { get; set; }

        public RoleRightViewModel(RestrictedRightBo right, RoleFullBo role)
        {
            Right = right;
            Role = role;
            var roleRight = Role.RoleRights.SingleOrDefault(r => r.RightId == Right.Id);
            if (roleRight != null)
                Allow = roleRight.Allow;
        }
    }
}