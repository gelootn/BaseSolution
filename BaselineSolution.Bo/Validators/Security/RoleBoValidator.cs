using BaselineSolution.Bo.Models.Security;
using FluentValidation;

namespace BaselineSolution.Bo.Validators.Security
{
    public class RoleBoValidator : AbstractValidator<RoleBo>
    {
        public RoleBoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
