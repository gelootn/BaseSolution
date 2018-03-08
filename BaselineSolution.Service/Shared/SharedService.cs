using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.Facade.Internal;
using BaselineSolution.Facade.Shared;

namespace BaselineSolution.Service.Shared
{
    public class SharedService : ISharedService
    {
        private readonly IGenericService<SystemLanguageBo> _systemLanguageService;

        public SharedService(IGenericService<SystemLanguageBo> systemLanguageService)
        {
            _systemLanguageService = systemLanguageService;
        }

        IGenericService<SystemLanguageBo> ISharedService.SystemLanguageService => _systemLanguageService;
    }
}
