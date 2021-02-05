using System.IO;

namespace Task4.BL.Contracts
{
    public interface IServerOperations
    {
        void StartOperations(FileSystemEventArgs args);
    }
}
