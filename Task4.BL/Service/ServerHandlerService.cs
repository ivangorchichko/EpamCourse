using System;
using System.Collections.Generic;
using System.IO;
using Task4.BL.Contracts;
using Task4.BL.CSVService.Model;
using Task4.DAL.DbContext;
using Task4.DAL.UnitOfWork.Contacts;
using Task4.DomainModel.DataModel;

namespace Task4.BL.Service
{
    public class ServerHandlerService : IServerHandlerService
    {
        public ServerHandlerService(ICsvParser parser, IUnitOfWork unitOfWork, ICatalogHandler catalogHandler)
        {
            _parser = parser;
            _context = new PurchaseContext();
            _unitOfWork = unitOfWork;
            _catalogHandler = catalogHandler;
        }

        private readonly ICsvParser _parser;
        private readonly PurchaseContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICatalogHandler _catalogHandler;

        public void StartOperations(FileSystemEventArgs args)
        {
            try
            {
                SaveDtoToDb(ParseCsvFile(args.FullPath));
                ChangeFileDirectory(args.FullPath, args.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in operation " + e);
                throw;
            }
        }

        private IEnumerable<PurchaseDto> ParseCsvFile(string path)
        {
            try
            {
                return _parser.ParseCsvFile(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception in parsing " + e);
                throw;
            }
        }

        private void SaveDtoToDb(IEnumerable<PurchaseDto> parseCollection)
        {
            foreach (var purchase in parseCollection)
            {
                try
                {
                    _unitOfWork.AddNewPurchase
                    (
                        new ClientEntity() {ClientName = purchase.Client},
                        new ProductEntity() {ProductName = purchase.Product, Price = purchase.Price,},
                        purchase.Date
                    );
                    _unitOfWork.SaveContext();
                    Console.WriteLine($"Purchase added, client = {purchase.Client}, product = {purchase.Product}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Purchase not valid" + e);
                }
            }
            _context.Dispose();
            _unitOfWork.Dispose(true);
        }

        private void ChangeFileDirectory(string filePath, string fileName)
        {
            _catalogHandler.BackUp(
                filePath.Remove(
                    filePath.IndexOf(fileName, StringComparison.Ordinal),
                    fileName.Length), 
                fileName);
        }
    }
}
