using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            _unitOfWork = unitOfWork;
            _catalogHandler = catalogHandler;
        }

        private readonly ICsvParser _parser;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICatalogHandler _catalogHandler;
        private readonly object _lockObj = new object();

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
            foreach (var purchaseDto in parseCollection)
            {
                try
                {
                    var purchase = CreatePurchase(purchaseDto);
                    lock (_lockObj)
                    {
                        _unitOfWork.Repository.Add(purchase);
                        _unitOfWork.SaveContext();
                    }
                    Console.WriteLine($"Purchase added, client = {purchaseDto.Client}, product = {purchaseDto.Product}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Purchase not valid" + e);
                }
            }
        }

        private void ChangeFileDirectory(string filePath, string fileName)
        {
            _catalogHandler.BackUp(
                filePath.Remove(
                    filePath.IndexOf(fileName, StringComparison.Ordinal),
                    fileName.Length),
                fileName);
        }

        private PurchaseEntity CreatePurchase(PurchaseDto purchaseDto)
        {
            var purchase = new PurchaseEntity();
           
            var product = _unitOfWork.Repository.
                Get<ProductEntity>(c => c.ProductName == purchaseDto.Product)
                .FirstOrDefault();
            var client = _unitOfWork.Repository
                .Get<ClientEntity>(c => c.ClientName == purchaseDto.Client)
                .FirstOrDefault();
            if (client != null)
            {
                purchase.ClientId = client.Id;
            }
            else
            {
                purchase.Client = new ClientEntity() {ClientName = purchaseDto.Client};
            }
            if (product != null)
            {
                purchase.ProductId = product.Id;
            }
            else
            {
                purchase.Product = new ProductEntity() {ProductName = purchaseDto.Product, Price = purchaseDto.Price};
            }
            purchase.Date = purchaseDto.Date;

            return purchase;
        }
    }
}
