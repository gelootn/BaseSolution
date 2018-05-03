using System;

namespace BaselineSolution.Framework.Logging
{
    public interface ILogging
    {
        void Info(string message);
        void Info(string message, params string[] parameters);
        void Warning(string message);
        void Warning(string message, params string[] parameters);
        void Error(string message);
        void Error(string message, params string[] parameters);
        void Error(Exception exception);
        void Debug(string message);
        void Debug(string message, params string[] parameters);
        void Trace(string message);
        void Trace(string message, params string[] parameters);
    }
}