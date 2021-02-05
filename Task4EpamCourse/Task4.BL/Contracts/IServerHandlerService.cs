using System.IO;

namespace Task4.BL.Contracts
{
    public interface IServerHandlerService
    {
        void StartOperations(FileSystemEventArgs args);
    }
}
