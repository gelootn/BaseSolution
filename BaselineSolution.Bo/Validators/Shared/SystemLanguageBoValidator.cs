using BaselineSolution.Bo.Models.Shared;
using FluentValidation;

namespace BaselineSolution.Bo.Validators.Shared
{
    public class SystemLanguageBoValidator : AbstractValidator<SystemLanguageBo>
    {
        public SystemLanguageBoValidator()
        {
            RuleFor(x => x.Culture).NotEmpty();
        }
    }
}
