using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources.Security;
using BaselineSolution.Bo.Validators.Security;
using BaselineSolution.Framework.Infrastructure;

namespace BaselineSolution.Bo.Models.Security
{
    public class UserSetPasswordBo : BaseBo
    {
        public UserSetPasswordBo()
        {
            Validator = new UserSetPasswordBoValidator();
        }

        [Display(ResourceType = typeof(UserBoResource), Name = "PasswordConfirm")]
        public string PasswordConfirm { get; set; } 

        [Display(ResourceType = typeof(UserBoResource), Name = "Password")]
        public string Password { get; set; }
    }
}

