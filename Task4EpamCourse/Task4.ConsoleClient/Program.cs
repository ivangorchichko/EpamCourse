using System;
using System.IO;
using System.Configuration;
using Serilog;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.BL.DependenciesConfig;
using Task4.BL.Enum;
using Task4.BL.Logger;
using Task4.BL.Service;

namespace Task4.ConsoleClient
{
    class Program
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(LoggerType.Console);
        private static readonly ICsvParser Parser = new CsvParser();
        private static readonly FileSystemWatcher FileWatcher =
            new FileSystemWatcher(ConfigurationManager.AppSettings.Get("receivedFolder"));
        private static readonly ICatalogWatcher Watcher
            = new CatalogWatcher(FileWatcher, Logger);
        private static readonly ITaskManager Manager
            = new TaskManager(Watcher,  Logger, Parser);
        
        static void Main()
        {
            AutofacConfiguration.ConfigureContainer(LoggerType.Console);
            using (Watcher)
            using (Manager)
            {
                Logger.Verbose("Application started");
                Watcher.Start();
                Console.ReadKey();
                Watcher.Stop();
                Logger.Verbose("Application stopped");
                Watcher.Dispose();
                Manager.Dispose();
            }
        }
    }
}
