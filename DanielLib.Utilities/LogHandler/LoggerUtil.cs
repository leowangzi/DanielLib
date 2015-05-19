using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Utilities.LogHandler
{
    /// <summary>
    /// 定义了一个新的生成日志的类
    /// 调用的方法为：
    /// LoggerUtil loggerUtil = new LoggerUtil("Sample", LoggingMode.ShareFile);
    /// loggerUtil.LogDebug("Message");
    /// </summary>
    public class LoggerUtil : ILogger
    {
        #region ILogger 属性

        public ILogger iLogger { get; private set; }

        public String Name { get; private set; }

        public LoggingMode LogMode { get; private set; }

        #endregion

        #region ILogger 构造函数

        public LoggerUtil(String name, LoggingMode loggingMode)
        {
            this.Name = name;
            this.LogMode = loggingMode;

            switch (this.LogMode)
            {
                case LoggingMode.Console:
                    iLogger = new ConsoleLogger(this.Name);
                    break;

                case LoggingMode.ShareFile:
                    iLogger = new ShareFileLogger(this.Name);
                    break;

                default:
                    iLogger = new ShareFileLogger(this.Name);
                    break;
            }
        }
        public LoggerUtil()
            : this("Log4Net", LoggingMode.ShareFile)
        {
            
        }

        #endregion

        #region ILogger 方法

        public void LogError(Exception e)
        {
            if (iLogger != null)
            {
                iLogger.LogError(e);
            } 
        }

        public void LogError(string title, Exception e)
        {
            if (iLogger != null)
            {
                iLogger.LogError(title, e);
            }
        }

        public void LogError(string message)
        {
            if (iLogger != null)
            {
                iLogger.LogError(message);
            }
        }

        public void LogDebug(string message)
        {
            if (iLogger != null)
            {
                iLogger.LogDebug(message);
            }
        }

        public void LogInfo(string message)
        {
            if (iLogger != null)
            {
                iLogger.LogInfo(message);
            }
        }

        public void LogPerf(string message)
        {
            if (iLogger != null)
            {
                iLogger.LogPerf(message);
            }
        }

        #endregion
    }
}
