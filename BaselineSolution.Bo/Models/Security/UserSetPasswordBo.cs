using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Validators;

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

