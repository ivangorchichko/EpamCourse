using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Task5.BL.Models;
using Task5.DAL.Repository.Contract;
using Task5.DomainModel.DataModel;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Manager;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.MapperWebHelper
{
    public static class MapperWebService
    {
        private static IMapper mapper = new Mapper(AutoMapperWebConfig.Configure());
        //Purchase//
        public static IEnumerable<PurchaseViewModel> GetPurchasesViewModels(IEnumerable<PurchaseDto> purchasesDto)
        {
            var purchases =
                mapper.Map<IEnumerable<PurchaseViewModel>>(purchasesDto);
            return purchases;
        }
        public static PurchaseDto GetPurchaseDto(PurchaseViewModel purchase)
        {
            return mapper.Map<PurchaseDto>(purchase);
        }

        //Client//
        public static IEnumerable<ClientViewModel> GetClientViewModels(IEnumerable<ClientDto> clientsDto)
        {
            var clients =
                mapper.Map<IEnumerable<ClientViewModel>>(clientsDto);
            return clients;
        }

        public static ClientDto GetClientDto(ClientViewModel client)
        {
            return mapper.Map<ClientDto>(client);
        }

        //Product//
        public static IEnumerable<ProductViewModel> GetProductViewModels(IEnumerable<ProductDto> productsDto)
        {
            var products =
                mapper.Map<IEnumerable<ProductViewModel>>(productsDto);
            return products;
        }
        public static ProductDto GetProductDto(ProductViewModel client)
        {
            return mapper.Map<ProductDto>(client);
        }

        //Manager//
        public static IEnumerable<ManagerViewModel> GetManagerViewModels(IEnumerable<ManagerDto> productsDto)
        {
            var products =
                mapper.Map<IEnumerable<ManagerViewModel>>(productsDto);
            return products;
        }
        public static ManagerDto GetManagerDto(ManagerViewModel client)
        {
            return mapper.Map<ManagerDto>(client);
        }
    }
}