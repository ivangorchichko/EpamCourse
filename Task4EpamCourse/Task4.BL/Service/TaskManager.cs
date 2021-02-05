using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;

namespace Task4.BL.Service
{
    public sealed class TaskManager : ITaskManager
    {
        private readonly ICsvParser _parser = new CsvParser();
        private readonly IRepository _repository;
        private readonly ICatalogHandler _catalogHandler =
            new CatalogHandler(ConfigurationManager.AppSettings.Get("serviceFolder"));
        private readonly SemaphoreSlim _semaphore;
        private readonly CancellationTokenSource _cancellationToken;

        public TaskManager(int tasksCount, ICatalogWatcher watcher, DbContext context)
        {
            _cancellationToken = new CancellationTokenSource();
            _semaphore = new SemaphoreSlim(tasksCount);
            _repository = new Repository(context);
            watcher.NewFileCreated += RunTasks;
            watcher.WatcherStopped += TaskStopped;
        }

        public void Dispose()
        {
            _cancellationToken?.Dispose();
            _semaphore?.Dispose();
            _repository?.Dispose();
        }

        private void RunTasks(object e, FileSystemEventArgs args)
        {
            var task = new Task(() =>
            {
                IServerHandlerService serverService = new ServerOperations(_parser, _repository, _catalogHandler);
                _semaphore.Wait();
                try
                {
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        serverService.StartOperations(args);
                    }
                    else
                    {
                        Console.WriteLine("Cancellation token is true");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Operation failed");
                }
                _semaphore.Release();
            }, _cancellationToken.Token);
            task.Start();
        }   

        private void TaskStopped(object e, EventArgs args)
        {
            _cancellationToken.Cancel();
        }
    }
}
