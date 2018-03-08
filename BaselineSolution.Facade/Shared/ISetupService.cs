using BaselineSolution.Bo.Models.Shared;
using BaselineSolution.Facade.Internal;

namespace BaselineSolution.Facade.Shared
{
    public interface ISharedService
    {
        IGenericService<SystemLanguageBo> SystemLanguageService { get; }
    }
}