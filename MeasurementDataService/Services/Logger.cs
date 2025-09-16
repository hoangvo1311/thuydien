using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class Logger
    {
        public static readonly NLog.Logger eLog = NLog.LogManager.GetCurrentClassLogger();

        public static void LogInfo(string message)
        {
            eLog.Info(message);
        }

        public static void LogError(string message)
        {
            eLog.Error(message);
        }
    }
}