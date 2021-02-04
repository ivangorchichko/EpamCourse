using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.DAL.UnitOfWork;
using Task4.DAL.UnitOfWork.Contacts;

namespace Task4.BL.Service
{
    public sealed class TaskManager : ITaskManager
    {
        public TaskManager(int tasksCount, ICatalogWatcher watcher)
        {
            _cancellationToken = new CancellationTokenSource();
            watcher.NewFileCreated += RunTasks;
            _semaphore = new SemaphoreSlim(tasksCount);
        }

        private readonly CancellationTokenSource _cancellationToken;
        private readonly ICsvParser _parser = new CsvParser();
        private readonly IUnitOfWork _uoW;
        private readonly ICatalogHandler _catalogHandler =
            new CatalogHandler(ConfigurationManager.AppSettings.Get("serviceFolder"));
        private readonly SemaphoreSlim _semaphore;
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
                IServerHandlerService serverService = new ServerHandlerService(_parser, _uoW, _catalogHandler);
                _semaphore.Wait();
                try
                {
                    serverService.StartOperations(args);
                }
                catch (Exception)
                {
                    throw new Exception("Operation failed");
                }
                _semaphore.Release();
            }, _cancellationToken.Token);
            task.Start();
        }
    }
}
