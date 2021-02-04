using System;
using System.IO;
using System.Configuration;
using Task4.BL.Contracts;
using Task4.BL.Service;

namespace Task4.ConsoleClient
{
    class Program
    {
        private static readonly ICatalogWatcher Watcher 
            = new CatalogWatcher(new FileSystemWatcher(ConfigurationManager.AppSettings.Get("sourceFolder")));

        private static readonly ITaskManager Manager 
            = new TaskManager(3, Watcher);

        static void Main(string[] args)
        {
            Watcher.Start();
            Console.ReadKey();
            Watcher.Stop();
            Watcher.Dispose(true);
            Manager.Dispose(true);
        }
    }
}
