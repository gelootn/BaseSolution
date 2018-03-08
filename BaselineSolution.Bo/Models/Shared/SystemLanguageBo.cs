using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Validators.Shared;

namespace BaselineSolution.Bo.Models.Shared
{
    public class SystemLanguageBo : BaseBo
    {
        public SystemLanguageBo()
        {
            Validator = new SystemLanguageBoValidator();
        }

        public string Culture { get; set; }
        public string Label { get; set; }
    }
}
