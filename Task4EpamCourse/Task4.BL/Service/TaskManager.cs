using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Task4.BL.Contracts;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;

namespace Task4.BL.Service
{
    public sealed class TaskManager : ITaskManager
    {
        private readonly ICsvParser _parser;
        private IRepository _repository;
        private readonly ILogger _logger;
        private readonly ICatalogHandler _catalogHandler =
            new CatalogHandler(ConfigurationManager.AppSettings.Get("processedFolder"));
        private readonly SemaphoreSlim _semaphore;
        private readonly CancellationTokenSource _cancellationToken;
        private const int TasksCount = 3;
        public TaskManager(ICatalogWatcher watcher, ILogger logger, ICsvParser parser)
        {
            _cancellationToken = new CancellationTokenSource();
            _semaphore = new SemaphoreSlim(TasksCount);
            _logger = logger;
            watcher.NewFileCreated += RunTasks;
            watcher.WatcherStopped += TaskStopped;
            _parser = parser;
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
                _repository = new Repository();
                IServerOperations serverService =
                    new ServerOperations(_parser, _repository, _catalogHandler, _logger);
                _semaphore.Wait();
                try
                {
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        _logger.Debug("Operations started");
                        serverService.StartOperations(args);
                    }
                    else
                    {
                        _logger.Warning("Cancellation token is true");
                    }
                }
                catch (Exception exception)
                {
                    _logger.Error("Operation failed " + exception);
                }
                _semaphore.Release();
            }, _cancellationToken.Token);
            task.Start();
        }

        private void TaskStopped(object e, EventArgs args)
        {
            try
            {
                _cancellationToken.Cancel();
                _logger.Debug("Task stopped");
            }
            catch (Exception exception)
            {
                _logger.Error("Can not stop task " + exception);
            }
        }
    }
}
