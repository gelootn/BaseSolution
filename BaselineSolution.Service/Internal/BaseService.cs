using NLog;

namespace BaselineSolution.Service.Internal
{
    public abstract class BaseService
    {
        protected Logger Log = LogManager.GetCurrentClassLogger();
    }
}
