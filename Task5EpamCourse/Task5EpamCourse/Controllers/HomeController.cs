using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Task5.BL.Contacts;
using Task5.DAL.DataBaseContext;
using Task5.DAL.Repository.Contract;
using Task5.DomainModel.DataModel;
using Task5EpamCourse.MapperWebHelper;

namespace Task5EpamCourse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IPurchaseService _purchaseService;

        public HomeController(IRepository repository, IPurchaseService purchaseService)
        {
            _repository = repository;
            _purchaseService = purchaseService;
        }

        public ActionResult Index()
        {
            return View(MapperWebService.GetPurchasesViewModels(_purchaseService.GetPurchaseDto())
                .OrderBy(d => d.Date)
                .ToList()
            );
        }
    }

    //заполнялка бд
    //using (var context = new PurchaseContext())
    //        {
    //            List<ClientEntity> clientEntities = new List<ClientEntity>()
    //                    {
    //                        new ClientEntity() {ClientName = "Mike",ClientTelephone  = "+375293647144", Id = 1},
    //                        new ClientEntity() {ClientName = "John", ClientTelephone  = "+375333647144", Id = 2},
    //                        new ClientEntity() {ClientName = "James", ClientTelephone  = "+375291234567", Id = 3},
    //                        new ClientEntity() {ClientName = "Rose", ClientTelephone  = "+375331234567", Id = 4},
    //                        new ClientEntity() {ClientName = "Michael", ClientTelephone  = "+375297654321", Id = 5},
    //                    };
    //List<ProductEntity> productEntities = new List<ProductEntity>()
    //                    {
    //                        new ProductEntity() {Price = 1.2, ProductName = "juice", Id = 1},
    //                        new ProductEntity() {Price = 1.0, ProductName = "water", Id = 2},
    //                        new ProductEntity() {Price = 3.6, ProductName = "mellow", Id = 3},
    //                        new ProductEntity() {Price = 1.2, ProductName = "apple", Id = 4},
    //                        new ProductEntity() {Price = 100.5, ProductName = "car", Id = 5},
    //                    };
    //List<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>()
    //                    {
    //                        new PurchaseEntity() {ClientId = clientEntities[0].Id,
    //                            Date = DateTime.Now,
    //                            ProductId = productEntities[0].Id,
    //                            Client = clientEntities[0],
    //                            Product = productEntities[0],
    //                            Id = 1,
    //                        },
    //                        new PurchaseEntity() {ClientId = clientEntities[1].Id,
    //                            Date = DateTime.Now,
    //                            ProductId = productEntities[1].Id,
    //                            Client = clientEntities[1],
    //                            Product = productEntities[1],
    //                            Id = 2,
    //                        },
    //                        new PurchaseEntity() {ClientId = clientEntities[2].Id,
    //                            Date = DateTime.Now,
    //                            ProductId = productEntities[2].Id,
    //                            Client = clientEntities[2],
    //                            Product = productEntities[2],
    //                            Id = 3,
    //                        },
    //                        new PurchaseEntity() {ClientId = clientEntities[3].Id,
    //                            Date = DateTime.Now,
    //                            ProductId = productEntities[3].Id,
    //                            Client = clientEntities[3],
    //                            Product = productEntities[3],
    //                            Id = 4,
    //                        },
    //                        new PurchaseEntity() {ClientId = clientEntities[4].Id,
    //                            Date = DateTime.Now,
    //                            ProductId = productEntities[4].Id,
    //                            Client = clientEntities[4],
    //                            Product = productEntities[4],
    //                            Id = 5,
    //                        },
    //                        new PurchaseEntity() {ClientId = clientEntities[4].Id,
    //                            Date = DateTime.Now,
    //                            ProductId = productEntities[1].Id,
    //                            Client = clientEntities[4],
    //                            Product = productEntities[1],
    //                            Id = 6,
    //                        },
    //                    };
    //var a = new PurchaseEntity()
    //{
    //    ClientId = 1,
    //    Date = DateTime.Now,
    //    ProductId = 1,
    //    Client = new ClientEntity() { ClientName = "Nick", ClientTelephone = "+375333647144", Id = 1 },
    //    Product = new ProductEntity() { ProductName = "juice", Price = 1.2, Id = 1 },
    //    Id = 1,
    //};
    ////context.Purchases.Add(a);
    //context.Purchases.AddRange(purchaseEntities);
    //            //context.Products.AddRange(productEntities);
    //           // context.Clients.AddRange(clientEntities);
    //            //context.Clients.Add(new ClientEntity()
    //            //    {ClientName = "Nick", ClientTelephone = "+375293647144", Id = 1, Purchases = new List<PurchaseEntity>()});
    //            context.SaveChanges();
}