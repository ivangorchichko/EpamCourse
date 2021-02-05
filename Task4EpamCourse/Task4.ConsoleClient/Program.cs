using System;
using System.IO;
using System.Configuration;
using Serilog;
using Serilog.Core;
using Task4.BL.Contracts;
using Task4.BL.Service;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Model;
using Task4.DomainModel.DataModel;

namespace Task4.ConsoleClient
{
    class Program
    {
        private static readonly PurchaseContext Context = new PurchaseContext();

        private static readonly ILogger Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Console()
            .WriteTo.File(ConfigurationManager.AppSettings.Get("loggerFile"),
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
            .CreateLogger();

        private static readonly ICatalogWatcher Watcher
            = new CatalogWatcher(new FileSystemWatcher(ConfigurationManager.AppSettings.Get("receivedFolder")), Logger);

        private static readonly ITaskManager Manager
            = new TaskManager(3, Watcher, Context, Logger);
        
        static void Main(string[] args)
        {
            using (Watcher)
            using (Manager)
            {
                Watcher.Start();
                Console.ReadKey();
                Watcher.Stop();
                Watcher.Dispose();
                Manager.Dispose();
            }
        }
    }
}
