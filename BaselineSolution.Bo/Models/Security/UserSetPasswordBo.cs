using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Validators;
using BaselineSolution.Bo.Validators.Security;

namespace BaselineSolution.Bo.Models.Security
{
    public class UserSetPasswordBo : BaseBo
    {
        public UserSetPasswordBo()
        {
            Validator = new UserSetPasswordBoValidator();
        }
        

        public string NewPassword { get; set; }
    }
}

