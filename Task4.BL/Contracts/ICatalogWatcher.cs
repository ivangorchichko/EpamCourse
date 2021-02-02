using System;
using System.IO;

namespace Task4.BL.Contracts
{
    public interface ICatalogWatcher 
    {
        event EventHandler<FileSystemEventArgs> NewFileCreated;

        void Start();

        void Stop();

        void Dispose(bool disposing);
    }
}
