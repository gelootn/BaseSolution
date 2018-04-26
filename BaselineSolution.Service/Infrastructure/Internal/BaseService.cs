using BaselineSolution.Facade.Internal;
using NLog;

namespace BaselineSolution.Service.Infrastructure.Internal
{
    public abstract class BaseService : IService
    {
        protected Logger Log = LogManager.GetCurrentClassLogger();
    }
}
