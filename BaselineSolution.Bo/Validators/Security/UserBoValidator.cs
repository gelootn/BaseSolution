using BaselineSolution.Bo.Models.Security;
using BaselineSolution.Bo.Resources;
using FluentValidation;

namespace BaselineSolution.Bo.Validators.Security
{
    public class UserBoValidator : AbstractValidator<UserBo>
    {
        public UserBoValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).Must(PasswordValidator.ValidatePassword).WithMessage(BoResources.PasswordLength).When(x => x.IsNew);
            RuleFor(x => x.PasswordConfirm).Matches(x => x.PasswordConfirm).When(x => x.IsNew);

        }
    }
}
