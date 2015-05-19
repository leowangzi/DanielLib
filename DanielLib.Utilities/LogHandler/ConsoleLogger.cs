using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Utilities.LogHandler
{
    internal class ConsoleLogger : ILogger
    {
        #region ILogger 属性

        public String Name { get; private set; }

        #endregion

        #region ILogger 构造函数

        public ConsoleLogger(String name)
        {
            this.Name = name;
        }

        public ConsoleLogger()
            : this("ConsoleLog4Net")
        {

        }

        #endregion

        #region ILogger 方法

        public void LogError(Exception e)
        {
            Console.WriteLine(Name + " : " + e.Message + " " + e.StackTrace);
            if (e.InnerException != null)
            {
                Console.WriteLine(e.InnerException.Message + " " + e.InnerException.StackTrace);
            }
        }

        public void LogError(string title, Exception e)
        {
            Console.WriteLine(Name + " : " + title + " " + e.Message + " " + e.StackTrace);
            if (e.InnerException != null)
            {
                Console.WriteLine(e.InnerException.Message + " " + e.InnerException.StackTrace);
            }
        }

        public void LogError(string message)
        {
            Console.WriteLine(Name + " : " + message);
        }

        public void LogDebug(string message)
        {
            Console.WriteLine(Name + " : " + message);
        }

        public void LogInfo(string message)
        {
            Console.WriteLine(Name + " : " + message);
        }

        public void LogPerf(string message)
        {
            Console.WriteLine(Name + " : " + message);
        }

        #endregion
    }
}
