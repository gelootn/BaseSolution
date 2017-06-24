using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Resources;
using FluentValidation;

namespace BaselineSolution.Bo.Validators
{
    public class UserSetPasswordBoValidator : AbstractValidator<UserSetPasswordBo>
    {
        public UserSetPasswordBoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
            RuleFor(x => x.NewPassword).NotEmpty().Length(min: 6, max: 255).WithMessage(BoResources.PasswordLength);
        }    
    }
}
