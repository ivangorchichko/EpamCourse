using System.Configuration;
using Serilog;
using Task4.BL.Enum;

namespace Task4.BL.Logger
{
    public static class LoggerFactory
    {
        public static ILogger GetLogger(LoggerType type)
        {
            switch (type)
            {
                case LoggerType.Console:
                {
                    return new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .WriteTo.Console()
                        .CreateLogger();
                }
                case LoggerType.File:
                {
                    return new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .WriteTo.File(ConfigurationManager.AppSettings.Get("loggerFile"),
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                        .CreateLogger();
                }
            }
            return null;
        }
    }
}
