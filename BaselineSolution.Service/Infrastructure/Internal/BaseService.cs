using BaselineSolution.Facade.Internal;
using BaselineSolution.Framework.Logging;


namespace BaselineSolution.Service.Infrastructure.Internal
{
    public abstract class BaseService : IService
    {
        protected ILogging Log;

        protected BaseService(ILogging log)
        {
            Log = log;
        }
    }
}
