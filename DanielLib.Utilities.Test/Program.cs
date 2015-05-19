using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DanielLib.Utilities.LogHandler;

namespace DanielLib.Utilities.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DebugLogHandler.logger.Debug("Message");
        }
    }
}
