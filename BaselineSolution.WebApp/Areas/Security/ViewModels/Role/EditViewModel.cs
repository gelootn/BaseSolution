using System.Collections.Generic;
using BaselineSolution.Bo.Models.Security;

namespace BaselineSolution.WebApp.Areas.Security.ViewModels.Role
{
    public class EditViewModel
    {
        public RoleBo RoleBo { get; set; }
        public List<RoleBo> AllowedRoles { get; set; }
    }
}