using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.DAL.Domain.Shared;
using BaselineSolution.Service.Translators.Internal;

namespace BaselineSolution.Service.Translators.Shared
{
    internal class SystemLanguageBoTranslator : ITranslator<SystemLanguageBo, SystemLanguage>
    {
        public SystemLanguageBo FromModel(SystemLanguage model)
        {
            var bo = new SystemLanguageBo();
            bo.Id = model.Id;
            bo.Culture = model.Culture;
            bo.Label = model.Label;
            return bo;
        }

        public SystemLanguage UpdateModel(SystemLanguageBo bo, SystemLanguage model)
        {
            model.Culture = bo.Culture;
            model.Label = bo.Label;

            return model;
        }
    }
}
