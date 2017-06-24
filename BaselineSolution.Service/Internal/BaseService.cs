using NLog;

namespace BaselineSolution.Service.Internal
{
    public abstract class BaseService : Service
    {
        protected Logger Log = LogManager.GetCurrentClassLogger();
    }
}
