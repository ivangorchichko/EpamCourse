using System;
using Task4.BL.CSVService;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;
using Task4.DAL.UnitOfWork;
using Task4.DAL.UnitOfWork.Contacts;
using Task4.DomainModel.DataModel;

namespace Task4.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            PurchaseContext context = new PurchaseContext();
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            IGenericRepository<PurchaseEntity> purchaseRepository = new GenericRepository<PurchaseEntity>(context);
            IGenericRepository<ProductEntity> productRepository = new GenericRepository<ProductEntity>(context);
            IGenericRepository<ClientEntity> clientRepository = new GenericRepository<ClientEntity>(context);
            var client = new ClientEntity() {ClientName = "John"};
            var pr = new PurchaseEntity()
            {
                Client = new ClientEntity() { ClientName = "John", },
                Product = new ProductEntity() { ProductName = "apple", Price = 4.1, },
                Date = DateTime.Today,
            };
           // purchaseRepository.Add(pr);
            clientRepository.Add(client);
            unitOfWork.SaveContext();
            var purchases = purchaseRepository.Get();
            foreach (var entity in purchases)
            {
                Console.WriteLine(entity.ToString());
            }
            string filePath = @"Data\\1.csv";
            CsvParser parser = new CsvParser();
            var a = parser.ParseCsvFile(filePath);
            foreach (var b in a)
            {
                Console.WriteLine(b);
            }
        }
    }
}
