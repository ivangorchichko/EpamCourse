using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog;
using Task4.BL.Contracts;
using Task4.BL.CSVService.Model;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;
using Task4.DomainModel.DataModel;

namespace Task4.BL.Service
{
    public class ServerOperations : IServerOperations
    {
        private readonly ICsvParser _parser;
        private readonly IRepository _repository;
        private readonly ICatalogHandler _catalogHandler;
        private readonly ILogger _logger;
        private readonly object _lockObj = new object();

        public ServerOperations(ICsvParser parser, IRepository repository, ICatalogHandler catalogHandler, ILogger logger)
        {
            _parser = parser;
            _repository = repository;
            _catalogHandler = catalogHandler;
            _logger = logger;
        }

        public void StartOperations(FileSystemEventArgs args)
        {
            try
            {
                SaveDtoToDb(ParseCsvFile(args.FullPath));
                _logger.Debug("Purchases added to Db");
                MoveFile(args.FullPath, args.Name);
            }
            catch (Exception e)
            {
                _logger.Error("Exception in operation " + e);
                throw;
            }
        }

        private IEnumerable<PurchaseDto> ParseCsvFile(string path)
        {
            try
            {
                var DtoCollection = _parser.ParseCsvFile(path);
                _logger.Debug("Parsing completed");
                return DtoCollection;
            }
            catch (Exception e)
            {
                _logger.Error("Exception in parsing " + e);
                throw;
            }
        }

        private void SaveDtoToDb(IEnumerable<PurchaseDto> parseCollection)
        {
            foreach (var purchaseDto in parseCollection)
            {
                try
                {
                    lock (_lockObj)
                    {
                        var purchase = CreatePurchase(purchaseDto);
                        _repository.Add(purchase);
                    }
                    _logger.Verbose($"Purchase added, client = {purchaseDto.Client}, product = {purchaseDto.Product}");
                }
                catch (Exception e)
                {
                    _logger.Error("Purchase not valid " + e);
                }
            }
        }

        private void MoveFile(string filePath, string fileName)
        {
            try
            {
                _catalogHandler.MoveFile(
                           filePath.Remove(
                               filePath.IndexOf(fileName, StringComparison.Ordinal),
                               fileName.Length),
                           fileName);
                _logger.Debug("File directory was change");
            }
            catch (Exception e)
            {
                _logger.Error("File can not change directory " + e);
            }

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
                purchase.Client = new ClientEntity() { ClientName = purchaseDto.Client };
            }
            if (product != null)
            {
                purchase.ProductId = product.Id;
            }
            else
            {
                purchase.Product = new ProductEntity() { ProductName = purchaseDto.Product, Price = purchaseDto.Price };
            }
            purchase.Date = purchaseDto.Date;

            return purchase;
        }
    }
}
