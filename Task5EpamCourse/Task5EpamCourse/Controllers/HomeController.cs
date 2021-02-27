using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Service;
using Task5.DAL.Repository.Contract;
using Task5.DAL.Repository.Model;
using Task5.DomainModel.DataModel;
using Task5EpamCourse.MapperWebHelper;
using Task5EpamCourse.Models;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.Controllers
{
    public class HomeController : Controller
    {
        private static readonly IRepository Repository = new Repository();
        private static readonly IPurchaseService PurchaseService = new PurchaseService(Repository);
        public ActionResult Index()
        {
            return View(MapperWebService.GetPurchasesViewModels(PurchaseService.GetPurchaseDto())
                .OrderBy(d => d.Date)
                .ToList()
            );
        }
    }

    //заполнялка бд
    //using (var context = new PurchaseContext())
    //        {
    //            List<ClientEntity> clientEntities = new List<ClientEntity>()
    //            {
    //                new ClientEntity() {ClientName = "Mike", Id = 1},
    //                new ClientEntity() {ClientName = "John",Id = 2},
    //                new ClientEntity() {ClientName = "James", Id = 3},
    //                new ClientEntity() {ClientName = "Rose", Id = 4},
    //                new ClientEntity() {ClientName = "Michael", Id = 5},
    //            };
    //List<ProductEntity> productEntities = new List<ProductEntity>()
    //            {
    //                new ProductEntity() {Price = 1.2, ProductName = "juice", Id = 1},
    //                new ProductEntity() {Price = 1.0, ProductName = "water", Id = 2},
    //                new ProductEntity() {Price = 3.6, ProductName = "mellow", Id = 3},
    //                new ProductEntity() {Price = 1.2, ProductName = "apple", Id = 4},
    //                new ProductEntity() {Price = 100.5, ProductName = "car", Id = 5},
    //            };
    //List<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>()
    //            {
    //                new PurchaseEntity() {ClientId = clientEntities[0].Id,
    //                    Date = DateTime.Now,
    //                    ProductId = productEntities[0].Id,
    //                    Client = clientEntities[0],
    //                    Product = productEntities[0],
    //                    Id = 1,
    //                },
    //                new PurchaseEntity() {ClientId = clientEntities[1].Id,
    //                    Date = DateTime.Now,
    //                    ProductId = productEntities[1].Id,
    //                    Client = clientEntities[1],
    //                    Product = productEntities[1],
    //                    Id = 2,
    //                },
    //                new PurchaseEntity() {ClientId = clientEntities[2].Id,
    //                    Date = DateTime.Now,
    //                    ProductId = productEntities[2].Id,
    //                    Client = clientEntities[2],
    //                    Product = productEntities[2],
    //                    Id = 3,
    //                },
    //                new PurchaseEntity() {ClientId = clientEntities[3].Id,
    //                    Date = DateTime.Now,
    //                    ProductId = productEntities[3].Id,
    //                    Client = clientEntities[3],
    //                    Product = productEntities[3],
    //                    Id = 4,
    //                },
    //                new PurchaseEntity() {ClientId = clientEntities[4].Id,
    //                    Date = DateTime.Now,
    //                    ProductId = productEntities[4].Id,
    //                    Client = clientEntities[4],
    //                    Product = productEntities[4],
    //                    Id = 5,
    //                },
    //            };
    //productEntities[0].PurchaseId = purchaseEntities[0].Id;
    //            productEntities[1].PurchaseId = purchaseEntities[1].Id;
    //            productEntities[2].PurchaseId = purchaseEntities[2].Id;
    //            productEntities[3].PurchaseId = purchaseEntities[3].Id;
    //            productEntities[4].PurchaseId = purchaseEntities[4].Id;
    //            clientEntities[0].PurchaseId = purchaseEntities[0].Id;
    //            clientEntities[1].PurchaseId = purchaseEntities[1].Id;
    //            clientEntities[2].PurchaseId = purchaseEntities[2].Id;
    //            clientEntities[3].PurchaseId = purchaseEntities[3].Id;
    //            clientEntities[4].PurchaseId = purchaseEntities[4].Id;
    //            context.Purchases.AddRange(purchaseEntities);
    //            //context.Products.AddRange(productEntities);
    //            //context.Clients.AddRange(clientEntities);
    //            context.SaveChanges();
    //        }
}