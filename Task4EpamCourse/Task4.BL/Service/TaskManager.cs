using System;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Serilog;
using Task4.BL.Contracts;
using Task4.BL.CSVService;
using Task4.BL.DependenciesConfig;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;
using Serilog.Core;

namespace Task4.BL.Service
{
    public sealed class TaskManager : ITaskManager
    {
        private readonly IContainer _container = AutofucConfig.ConfigureContainer();
        private readonly ICsvParser _parser = new CsvParser();
        private readonly IRepository _repository;
        private readonly ICatalogHandler _catalogHandler =
            new CatalogHandler(ConfigurationManager.AppSettings.Get("processedFolder"));

        private readonly ILogger _logger;
        private readonly SemaphoreSlim _semaphore;
        private readonly CancellationTokenSource _cancellationToken;

        public TaskManager(int tasksCount, ICatalogWatcher watcher, DbContext context, ILogger logger)
        {
            _cancellationToken = new CancellationTokenSource();
            _semaphore = new SemaphoreSlim(tasksCount);
            _repository = new Repository(context);
            _logger = logger;
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
              //  var serverService = _container.Resolve<ServerOperations>();
                IServerOperations serverService = new ServerOperations(_parser, _repository, _catalogHandler, _logger);
                _semaphore.Wait();
                try
                {
                    if (!_cancellationToken.IsCancellationRequested)
                    {
                        serverService.StartOperations(args);
                        _logger.Debug("Operations started");
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
