using System;
using System.IO;

namespace Task4.BL.Contracts
{
    public interface ICatalogWatcher : IDisposable
    {
        event EventHandler<FileSystemEventArgs> NewFileCreated;
        event EventHandler WatcherStopped;

        void Start();

        void Stop();
    }
}
