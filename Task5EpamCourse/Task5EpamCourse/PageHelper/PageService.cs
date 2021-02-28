using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Task5.BL.Contacts;
using Task5.BL.Enums;
using Task5.DAL.Repository.Contract;
using Task5EpamCourse.MapperWebHelper;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Page;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;
using Task5EpamCourse.PageHelper.Contacts;

namespace Task5EpamCourse.PageHelper
{
    public class PageService : IPageService
    {
        private static IPurchaseService _purchaseService;
        private static IProductService _productService;
        private static IClientService _clientService;


        public PageService(IPurchaseService purchaseService, IClientService clientService, IProductService productService)
        {
            _clientService = clientService;
            _productService = productService;
            _purchaseService = purchaseService;
        }

        public ModelsInPageViewModel GetModelsInPageViewModel<TModel> (int page) where TModel : class
        {
            var modelsEntity = _purchaseService.GetPurchaseDto(page);
            var modelsPerPage = MapperWebService.GetPurchasesViewModels(modelsEntity);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = _purchaseService.GetPurchaseDto().ToList().Count };
            if (typeof(TModel) == typeof(PurchaseViewModel))
            {
                return new ModelsInPageViewModel() {PageInfo = pageInfo, Purchases = modelsPerPage as IEnumerable<PurchaseViewModel>};
            }
            if (typeof(TModel) == typeof(ProductViewModel))
            {
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Products = modelsPerPage as IEnumerable<ProductViewModel> };
            }
            if (typeof(TModel) == typeof(ClientViewModel))
            {
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Clients = modelsPerPage as IEnumerable<ClientViewModel> };
            }

            return null;
        }

        public ModelsInPageViewModel GetFilteredModelsInPageViewModel<TModel>(TextFieldFilter filter, string fieldString, int page) where TModel : class
        {
            var modelsEntity = _purchaseService.GetFilteredPurchaseDto(filter, fieldString, page);
            var pagingModelEntity = modelsEntity.Skip((page - 1) * 3).Take(3).ToList();
            var modelsPerPage = MapperWebService.GetPurchasesViewModels(pagingModelEntity);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = modelsEntity.ToList().Count };
            if (typeof(TModel) == typeof(PurchaseViewModel))
            {
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Purchases = modelsPerPage as IEnumerable<PurchaseViewModel> };
            }
            if (typeof(TModel) == typeof(ProductViewModel))
            {
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Products = modelsPerPage as IEnumerable<ProductViewModel> };
            }
            if (typeof(TModel) == typeof(ClientViewModel))
            {
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Clients = modelsPerPage as IEnumerable<ClientViewModel> };
            }

            return null;
        }

        //public static PurchasesInPageViewModel GetPurchasesPages(IEnumerable<PurchaseViewModel> purchases, int page)
        //{
        //    int pageSize = 3;
        //    IEnumerable<PurchaseViewModel> purchasesPerPages = purchases.Skip((page - 1) * pageSize).Take(pageSize);
        //    PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = purchases.ToList().Count };
        //    return new PurchasesInPageViewModel { PageInfo = pageInfo, Purchases = purchasesPerPages };
        //}

        public static ClientInPageViewModel GetClientsPages(IEnumerable<ClientViewModel> clients, int page)
        {
            int pageSize = 3;
            IEnumerable<ClientViewModel> clientsPerPages = clients.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = clients.ToList().Count };
            return new ClientInPageViewModel() { PageInfo = pageInfo, Clients = clientsPerPages };
        }

        public static ProductsInPageViewModel GetProductPages(IEnumerable<ProductViewModel> products, int page)
        {
            int pageSize = 3;
            IEnumerable<ProductViewModel> productsPerPages = products.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = products.ToList().Count };
            return new ProductsInPageViewModel() { PageInfo = pageInfo, Products = productsPerPages };
        }
    }
}