using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.Bo.Validators.Security;

namespace BaselineSolution.Bo.Models.Security
{
    public class UserSetPasswordBo : BaseBo
    {
        public UserSetPasswordBo()
        {
            Validator = new UserSetPasswordBoValidator();
        }
        
        [Display(ResourceType = typeof(UserBoResource), Name = "Password")]
        public string NewPassword { get; set; }
    }
}

