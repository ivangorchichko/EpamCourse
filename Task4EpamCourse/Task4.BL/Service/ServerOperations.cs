using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Task4.BL.Contracts;
using Task4.BL.CSVService.Model;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;
using Task4.DomainModel.DataModel;

namespace Task4.BL.Service
{
    public class ServerOperations : IServerHandlerService
    {
        private readonly ICsvParser _parser;
        private readonly IRepository _repository;
        private readonly ICatalogHandler _catalogHandler;
        private readonly object _lockObj = new object();

        public ServerOperations(ICsvParser parser, IRepository repository, ICatalogHandler catalogHandler)
        {
            _parser = parser;
            _repository = repository;
            _catalogHandler = catalogHandler;
        }

        public void StartOperations(FileSystemEventArgs args)
        {
            try
            {
                SaveDtoToDb(ParseCsvFile(args.FullPath));
                MoveFile(args.FullPath, args.Name);
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
                        _repository.Add(purchase);
                    }
                    Console.WriteLine($"Purchase added, client = {purchaseDto.Client}, product = {purchaseDto.Product}");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Purchase not valid" + e);
                }
            }
        }

        private void MoveFile(string filePath, string fileName)
        {
            _catalogHandler.MoveFile(
                filePath.Remove(
                    filePath.IndexOf(fileName, StringComparison.Ordinal),
                    fileName.Length),
                fileName);
        }

        private PurchaseEntity CreatePurchase(PurchaseDto purchaseDto)
        {
            var purchase = new PurchaseEntity();
           
            var product = _repository.
                Get<ProductEntity>(c => c.ProductName == purchaseDto.Product)
                .FirstOrDefault();
            var client = _repository
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
