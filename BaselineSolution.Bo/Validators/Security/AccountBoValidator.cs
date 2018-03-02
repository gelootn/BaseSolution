using BaselineSolution.Bo.Models.Security;
using FluentValidation;

namespace BaselineSolution.Bo.Validators.Security
{
    public class AccountBoValidator : AbstractValidator<AccountBo>
    {
        public AccountBoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
