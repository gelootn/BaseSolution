using NLog;

namespace BaselineSolution.Service.Infrastructure.Internal
{
    public abstract class BaseService : Service
    {
        protected Logger Log = LogManager.GetCurrentClassLogger();
    }
}
