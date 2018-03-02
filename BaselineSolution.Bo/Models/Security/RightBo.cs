using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources.Security;

namespace BaselineSolution.Bo.Models.Security
{
    public class RightBo : BaseBo
    {
        [Display(ResourceType = typeof(RightBoResource), Name = "ParentId")]
        public virtual int? ParentId { get; set; }
        [Display(ResourceType = typeof(RightBoResource), Name = "Key")]
        public virtual string Key { get; set; }
        public virtual DisplayObject Parent { get; set; }
    }
}
