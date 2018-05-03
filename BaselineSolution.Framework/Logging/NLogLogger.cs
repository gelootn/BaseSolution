using System;
using NLog;

namespace BaselineSolution.Framework.Logging
{
    public class NLogLogger : ILogging
    {
        private readonly ILogger _logger;

        public NLogLogger(ILogger logger)
        {
            _logger = logger;
        }


        void ILogging.Info(string message)
        {
            _logger.Info(message);
        }

        void ILogging.Info(string message, params string[] parameters)
        {
            _logger.Info(message, parameters);
        }

        void ILogging.Warning(string message)
        {
            _logger.Warn(message);
        }

        void ILogging.Warning(string message, params string[] parameters)
        {
            _logger.Warn(message, parameters);
        }

        void ILogging.Error(string message)
        {
            _logger.Error(message);
        }

        void ILogging.Error(string message, params string[] parameters)
        {
            _logger.Error(message, parameters);
        }

        void ILogging.Error(Exception exception)
        {
            _logger.Error(exception);
        }

        void ILogging.Debug(string message)
        {
            _logger.Debug(message);
        }

        void ILogging.Debug(string message, params string[] parameters)
        {
            _logger.Debug(message, parameters);
        }

        void ILogging.Trace(string message)
        {
            _logger.Trace(message);
        }

        void ILogging.Trace(string message, params string[] parameters)
        {
            _logger.Trace(message, parameters);
        }
    }
}
