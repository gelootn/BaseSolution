using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.Framework.Infrastructure;

namespace BaselineSolution.Bo.Models.Security
{
    public class RoleRightBo : BaseBo
    {

        [Display(ResourceType = typeof(RoleRightBoResourceBo), Name = "RoleId")]
        public virtual int RoleId { get; set; }
        [Display(ResourceType = typeof(RoleRightBoResourceBo), Name = "RightId")]
        public virtual int RightId { get; set; }
        [Display(ResourceType = typeof(RoleRightBoResourceBo), Name = "Allow")]
        public virtual bool? Allow { get; set; }

        public virtual DisplayObject Role { get; set; }
        public virtual DisplayObject Right { get; set; }
    }
}
