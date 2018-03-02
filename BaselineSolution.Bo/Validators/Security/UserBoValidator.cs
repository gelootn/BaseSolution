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
            RuleFor(x => x.Password).NotEmpty().Length(min: 6, max: 255).WithMessage(BoResources.PasswordLength).When(x => x.IsNew);
            RuleFor(x => x.Password).Empty().When(x => !x.IsNew);
        }
    }
}
