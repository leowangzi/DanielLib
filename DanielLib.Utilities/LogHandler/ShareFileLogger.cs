using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;
using log4net.Appender;
using log4net.Layout;
using log4net.Core;

namespace DanielLib.Utilities.LogHandler
{
    internal class ShareFileLogger : ILogger
    {
        #region ILogger 属性

        public String Name { get; private set; }

        private ILog logger;

        private readonly String conversionPattern = "%d [%t] %-5p %c [%x] - %m%n";

        #endregion

        #region ILogger 构造函数

        public ShareFileLogger(String name)
        {
            this.Name = name;
            this.Setup();
        }

        public ShareFileLogger()
            : this("ShareFileLog4Net")
        {

        }

        #endregion

        #region ILogger 方法

        private void Setup()
        {
            logger = LogManager.GetLogger(this.Name);

            string log4NetLog = Path.Combine("Log", "log4Net.log");
            if (!Path.IsPathRooted(log4NetLog))
                log4NetLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, log4NetLog);

            log4net.Repository.Hierarchy.Hierarchy repository = (log4net.Repository.Hierarchy.Hierarchy)LogManager.GetRepository();
            repository.Name = "milo-match-up.com.sg";

            TraceAppender newAppender = new TraceAppender();
            PatternLayout layout = new PatternLayout
            {
                ConversionPattern = conversionPattern
            };
            layout.ActivateOptions();
            newAppender.Layout = layout;
            newAppender.ActivateOptions();
            repository.Root.AddAppender(newAppender);
            RollingFileAppender appender2 = new RollingFileAppender
            {
                Layout = layout,
                AppendToFile = true,
                RollingStyle = RollingFileAppender.RollingMode.Date,
                StaticLogFileName = true,
                File = log4NetLog
            };
            appender2.ActivateOptions();
            repository.Root.AddAppender(appender2);
            repository.Root.Level = Level.All;
            repository.Configured = true;
        }

        public void LogError(Exception e)
        {
            logger.Error("", e);
        }

        public void LogError(string title, Exception e)
        {
            logger.Error(title, e);
        }

        public void LogError(string message)
        {
            logger.Error(this.Name + " : " + message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(this.Name + " : " + message);
        }

        public void LogInfo(string message)
        {
            logger.Info(this.Name + " : " + message);
        }

        public void LogPerf(string message)
        {
            logger.Info(this.Name + " : " + message);
        }

        #endregion
    }
}
