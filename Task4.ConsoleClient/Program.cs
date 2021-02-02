using System;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.BL.Service;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;
using Task4.DAL.UnitOfWork;
using Task4.DAL.UnitOfWork.Contacts;
using Task4.DomainModel.DataModel;

namespace Task4.ConsoleClient
{
    class Program
    {
        private static readonly ICatalogWatcher Watcher 
            = new CatalogWatcher(new FileSystemWatcher(ConfigurationManager.AppSettings.Get("sourceFolder")));

        private static readonly ITaskManager Manager 
            = new TaskManager(new CustomTaskScheduler(3), Watcher);
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
