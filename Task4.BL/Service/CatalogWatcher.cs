using System;
using System.IO;
using Task4.BL.Contracts;

namespace Task4.BL.Service
{
    public sealed class CatalogWatcher : ICatalogWatcher
    {
        public CatalogWatcher(FileSystemWatcher watcher)
        {
            _watcher = watcher;
            _watcher.Created += OnNewFileCreated;
        }

        public event EventHandler<FileSystemEventArgs> NewFileCreated;

        private bool _disposed;
        private readonly FileSystemWatcher _watcher;

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
        }

        public void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                NewFileCreated = null;
                _watcher.Created -= OnNewFileCreated;
                _watcher.Dispose();
            }
            _disposed = true;
        }

        private void OnNewFileCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine($"File: {e.Name} {e.ChangeType}");
            NewFileCreated?.Invoke(source, e);
        }
    }
}
