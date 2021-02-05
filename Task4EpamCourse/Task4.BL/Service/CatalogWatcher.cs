using System;
using System.IO;
using Serilog;
using Task4.BL.Contracts;

namespace Task4.BL.Service
{
    public sealed class CatalogWatcher : ICatalogWatcher
    {
        private readonly FileSystemWatcher _watcher;
        private readonly ILogger _logger;
        public CatalogWatcher(FileSystemWatcher watcher, ILogger logger)
        {
            _watcher = watcher;
            _logger = logger;
            _watcher.Created += OnNewFileCreated;
        }

        public event EventHandler<FileSystemEventArgs> NewFileCreated;
        public event EventHandler WatcherStopped;

        public void Start()
        {
            _watcher.NotifyFilter = NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.FileName
                                   | NotifyFilters.DirectoryName;

            _watcher.Filter = "*.csv";
            _watcher.EnableRaisingEvents = true;
        }

        public void Stop()
        {
            _watcher.EnableRaisingEvents = false;
            OnWatcherStopped(this);
        }

        public void Dispose()
        {
            _watcher?.Dispose();
        }

        private void OnNewFileCreated(object source, FileSystemEventArgs e)
        {
            _logger.Verbose($"File: {e.Name} {e.ChangeType}");
            NewFileCreated?.Invoke(source, e);
        }

        private void OnWatcherStopped(object sender)
        {
            WatcherStopped?.Invoke(sender, EventArgs.Empty);
            _logger.Verbose("Watcher stopped");
        }
    }
}
