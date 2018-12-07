using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.Bo.Validators.Security;
using BaselineSolution.Framework.Infrastructure;

namespace BaselineSolution.Bo.Models.Security
{
    public class RoleBo : BaseBo
    {
        public RoleBo()
        {
            Validator = new RoleBoValidator();
        }

        [Display(ResourceType = typeof(RoleBoResource), Name = "Name")]
        public virtual string Name { get; set; }
        [Display(ResourceType = typeof(RoleBoResource), Name = "ParentId")]
        public virtual int? ParentId { get; set; }
        public virtual DisplayObject Parent { get; set; }

        
    }
}
