using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Resources;
using FluentValidation;

namespace BaselineSolution.Bo.Validators.Security
{
    public class UserSetPasswordBoValidator : AbstractValidator<UserSetPasswordBo>
    {
        public UserSetPasswordBoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.Password).Must(PasswordValidator.ValidatePassword).WithMessage(BoResources.PasswordLength);
        }    
    }
}
