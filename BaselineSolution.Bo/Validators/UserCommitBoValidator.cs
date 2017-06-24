using BaselineSolution.Bo.Models.Security;
using FluentValidation;

namespace BaselineSolution.Bo.Validators
{
    public class UserCommitBoValidator : AbstractValidator<UserCommitBo>
    {
        public UserCommitBoValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
