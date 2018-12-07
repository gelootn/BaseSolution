using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.Bo.Validators.Security;
using BaselineSolution.Framework.Infrastructure;
using FluentValidation;

namespace BaselineSolution.Bo.Models.Security
{
    public class AccountBo : BaseBo
    {
        public AccountBo()
        {
            Validator = new AccountBoValidator();
        }

        [Display(ResourceType = typeof(AccountBoResource), Name = "Name")]
        public virtual string Name { get; set; }
        [Display(ResourceType = typeof(AccountBoResource), Name = "Description")]
        public virtual string Description { get; set; }
        [Display(ResourceType = typeof(AccountBoResource), Name = "Parent")]
        public virtual int? ParentId { get; set; }

        public virtual DisplayObject Parent { get; set; }
        public virtual ICollection<DisplayObject> Children { get; set; }

    }
}
