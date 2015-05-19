using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DanielLib.Utilities.LogHandler
{
    /// <summary>
    /// 接口
    /// </summary>
    public interface ILogger
    {
        String Name { get; }

        void LogError(Exception e);

        void LogError(string title, Exception e);

        void LogError(string message);

        void LogDebug(string message);

        void LogInfo(string message);

        void LogPerf(string message);
    }
}
