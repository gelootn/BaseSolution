using System.ComponentModel.DataAnnotations;
using BaselineSolution.Bo.Internal;
using BaselineSolution.Bo.Resources;
using BaselineSolution.Bo.Resources.Shared;
using BaselineSolution.Bo.Validators.Shared;
using BaselineSolution.Framework.Infrastructure;

namespace BaselineSolution.Bo.Models.Shared
{
    public class SystemLanguageBo : BaseBo
    {
        public SystemLanguageBo()
        {
            Validator = new SystemLanguageBoValidator();
        }

        [Display(ResourceType = typeof(SystemLanguageBoResource), Name = "Culture_text")]
        [Required(ErrorMessageResourceType = typeof(BoResources), ErrorMessageResourceName = "Required")]
        public string Culture { get; set; }
        [Display(ResourceType = typeof(SystemLanguageBoResource), Name = "Label")]
        public string Label { get; set; }
    }
}
