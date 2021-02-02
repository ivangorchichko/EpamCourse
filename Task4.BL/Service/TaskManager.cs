using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.DAL.DbContext;
using Task4.DAL.UnitOfWork;

namespace Task4.BL.Service
{
    public sealed class TaskManager : ITaskManager
    {
        public TaskManager(CustomTaskScheduler scheduler, ICatalogWatcher watcher)
        {
            _scheduler = scheduler;
            _cancellationToken = new CancellationTokenSource();
            watcher.NewFileCreated += RunTasks;
        }

        private readonly CustomTaskScheduler _scheduler;
        private readonly CancellationTokenSource _cancellationToken;
        private bool _disposed;

        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                _cancellationToken.Dispose();
            }
            _disposed = true;
        }

        private void RunTasks(object e, FileSystemEventArgs args)
        {
            var task = new Task(() =>
            {
                IServerHandlerService serverService = new ServerHandlerService(
                    new CsvParser(),
                    new UnitOfWork(new PurchaseContext()),
                    new CatalogHandler(ConfigurationManager.AppSettings.Get("serviceFolder"))
                    );
                try
                {
                    serverService.StartOperations(args);
                }
                catch (Exception)
                {
                    throw new Exception("Operation failed");
                }
            }, _cancellationToken.Token);
            task.Start(_scheduler);
        }
    }
}

