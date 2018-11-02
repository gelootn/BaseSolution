using Autofac;
using BaselineSolution.Framework.Logging;
using NLog;
using NLog.Config;

namespace BaselineSolution.IOC
{
    public class LogModule : Module
    {
        private readonly LoggingConfiguration _config;
        private readonly string _loggerName;

        public LogModule(LoggingConfiguration config, string loggerName)
        {
            _config = config;
            _loggerName = loggerName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            LogManager.Configuration = _config;

            builder.Register(c => new NLogLogger(LogManager.GetLogger(_loggerName))).As<ILogging>().InstancePerRequest();
            base.Load(builder);
        }
    }
}
