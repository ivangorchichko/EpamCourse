using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Serilog;

namespace Task5EpamCourse.Logger
{
    public static class LoggerFactory
    {
        public static ILogger GetLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(ConfigurationManager.AppSettings.Get("loggerFile"),
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}