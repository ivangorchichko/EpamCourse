using System;
using System.IO;
using Task4.BL.Contracts;

namespace Task4.BL.Service
{
    public class CatalogHandler : ICatalogHandler
    {
        private readonly string _directoryPath;
        public CatalogHandler(string directoryPath)
        {
            this._directoryPath = directoryPath;
        }
        public void MoveFile(string filePath, string fileName)
        {
            try
            {
                File.Move(filePath + "\\" + fileName, _directoryPath + "\\" + fileName);
            }
            catch (IOException)
            {
                throw new InvalidOperationException("cannot backup file");
            }
        }
    }
}
