using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using System.IO;

namespace DanielLib.Utilities.LogHandler
{
    /// <summary>
    /// 简单的生成日志文件的类
    /// 调用方法：
    /// DebugLogHandler.logger.Debug("Message");
    /// </summary>
    public class DebugLogHandler
    {
        public static readonly ILog logger;

        private static readonly String conversionPattern = "%d [%t] %-5p %c [%x] - %m%n";

        static DebugLogHandler()
        {
            logger = LogManager.GetLogger("log");

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
        public DebugLogHandler()
        {

        }
    }
}
