using System;
using System.Text;
using NLog;
using NLog.Config;
using NLog.Layouts;
using NLog.Targets;

namespace BaselineSolution.Framework.Logging
{
    public class NlogConfig
    {
        public static LoggingConfiguration GetConfig(string logName)
        {
            var config = new LoggingConfiguration();

            var webTarget = GetWebApiTarget(logName);
            var fileTarget = GetFileTarget(logName);

            config.AddTarget(fileTarget);
            //config.AddTarget(webTarget);

#if DEBUG
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, fileTarget));
            //config.LoggingRules.Add(new LoggingRule("*", LogLevel.Debug, webTarget));
#else
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, fileTarget));
            config.LoggingRules.Add(new LoggingRule("*", LogLevel.Info, webTarget));
#endif

            return config;
        }

        private static FileTarget GetFileTarget(string logName)
        {
            var fileTarget = new FileTarget(logName);
            var csvLayout = GetCsvLayout();

            fileTarget.FileName = "${basedir}/Log/" + logName + ".log";
            fileTarget.Layout = csvLayout;
#if DEBUG
            fileTarget.ArchiveEvery = FileArchivePeriod.Minute;
#else
            fileTarget.ArchiveEvery = FileArchivePeriod.Day;
#endif
            fileTarget.ArchiveNumbering = ArchiveNumberingMode.Rolling;
            fileTarget.MaxArchiveFiles = 60;
            fileTarget.ArchiveFileName = "${basedir}/Log/${#####}" + logName + ".log";
            return fileTarget;
        }

        private static WebServiceTarget GetWebApiTarget(string logName)
        {
            var webTarget = new WebServiceTarget($"Web_{logName}");
            webTarget.Url = new Uri("http://localhost:62406/api/v1/log");
            webTarget.Protocol = WebServiceProtocol.JsonPost;
            webTarget.IncludeBOM = false;
            webTarget.Encoding = Encoding.UTF8;
            webTarget.Parameters.Add(new MethodCallParameter("level", "${level:upperCase=true}", typeof(string)));
            webTarget.Parameters.Add(new MethodCallParameter("callsite", "${callsite}", typeof(string)));
            webTarget.Parameters.Add(new MethodCallParameter("message", "${message}", typeof(string)));
            webTarget.Parameters.Add(new MethodCallParameter("error", "${exception:format=toString}", typeof(string)));
            return webTarget;
        }

        private static CsvLayout GetCsvLayout()
        {
            var csvLayout = new CsvLayout();

            csvLayout.Delimiter = CsvColumnDelimiterMode.Tab;
            csvLayout.WithHeader = false;
            csvLayout.Columns.Add(new CsvColumn("time", Layout.FromString(@"${date:format=HH\:mm\:ss.fff}")));

            csvLayout.Columns.Add(new CsvColumn("level", Layout.FromString("${level:upperCase=true}")));
            csvLayout.Columns.Add(new CsvColumn("Caller", Layout.FromString("${callsite}")));
            csvLayout.Columns.Add(new CsvColumn("message", Layout.FromString("${message}")));
            csvLayout.Columns.Add(new CsvColumn("Error", Layout.FromString("${exception:format=toString}")));
            return csvLayout;
        }
    }
}