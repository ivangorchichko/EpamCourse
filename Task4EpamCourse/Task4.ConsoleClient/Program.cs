using System;
using System.IO;
using System.Configuration;
using Task4.BL.Contracts;
using Task4.BL.Service;
using Task4.DAL.DbContext;

namespace Task4.ConsoleClient
{
    class Program
    {
        private static readonly PurchaseContext context = new PurchaseContext();

        private static readonly ICatalogWatcher Watcher
            = new CatalogWatcher(new FileSystemWatcher(ConfigurationManager.AppSettings.Get("sourceFolder")));

        private static readonly ITaskManager Manager
            = new TaskManager(3, Watcher, context);

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
