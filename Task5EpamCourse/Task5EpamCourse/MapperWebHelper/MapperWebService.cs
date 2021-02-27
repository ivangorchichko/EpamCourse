using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Task5.BL.Models;
using Task5.DAL.Repository.Contract;
using Task5.DomainModel.DataModel;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.MapperWebHelper
{
    public static class MapperWebService
    {
        private static IMapper mapper = new Mapper(AutoMapperWebConfig.Configure());
        //Purchase//
        public static IEnumerable<IndexPurchaseViewModel> GetPurchasesViewModels(IEnumerable<PurchaseDto> purchasesDto)
        {
            var purchases =
                mapper.Map<IEnumerable<IndexPurchaseViewModel>>(purchasesDto);
            return purchases;
        }

        public static DetailsPurchaseViewModel GetDetailsPurchaseViewModel(CreatePurchaseViewModel createPurchase)
        {
            return mapper.Map<DetailsPurchaseViewModel>(createPurchase);
        }

        public static DetailsPurchaseViewModel GetDetailsPurchaseViewModel(IndexPurchaseViewModel purchase)
        {
            return mapper.Map<DetailsPurchaseViewModel>(purchase);
        }

        public static ModifyPurchaseViewModel GetModifyPurchaseViewModel(IndexPurchaseViewModel purchase)
        {
            return mapper.Map<ModifyPurchaseViewModel>(purchase);
        }

        public static PurchaseDto GetPurchaseDto(CreatePurchaseViewModel purchase)
        {
            return mapper.Map<PurchaseDto>(purchase);
        }
        public static PurchaseDto GetPurchaseDto(DeletePurchaseViewModel purchase)
        {
            return mapper.Map<PurchaseDto>(purchase);
        }
        public static PurchaseDto GetPurchaseDto(ModifyPurchaseViewModel purchase)
        {
            return mapper.Map<PurchaseDto>(purchase);
        }
        public static DeletePurchaseViewModel GetDeletePurchaseViewModel(IndexPurchaseViewModel purchase)
        {
            var entity = mapper.Map<DeletePurchaseViewModel>(purchase);
            return entity;
        }


        //Client//
        public static IEnumerable<IndexClientViewModel> GetClientViewModels(IEnumerable<ClientDto> clientsDto)
        {
            var clients =
                mapper.Map<IEnumerable<IndexClientViewModel>>(clientsDto);
            return clients;
        }

        public static DetailsClientViewModel GetDetailsClientViewModel(CreateClientViewModel createClient)
        {
            return mapper.Map<DetailsClientViewModel>(createClient);
        }

        public static DetailsClientViewModel GetDetailsClientViewModel(IndexClientViewModel client)
        {
            return mapper.Map<DetailsClientViewModel>(client);
        }

        public static ModifyClientViewModel GetModifyClientViewModel(IndexClientViewModel client)
        {
            return mapper.Map<ModifyClientViewModel>(client);
        }

        public static ClientDto GetClientDto(CreateClientViewModel client)
        {
            return mapper.Map<ClientDto>(client);
        }
        public static ClientDto GetClientDto(DeleteClientViewModel client)
        {
            return mapper.Map<ClientDto>(client);
        }
        public static ClientDto GetClientDto(ModifyClientViewModel client)
        {
            return mapper.Map<ClientDto>(client);
        }
        public static DeleteClientViewModel GetDeleteClientViewModel(IndexClientViewModel client)
        {
            var entity = mapper.Map<DeleteClientViewModel>(client);
            return entity;
        }

        //Product//
        public static IEnumerable<IndexProductViewModel> GetProductViewModels(IEnumerable<ProductDto> productsDto)
        {
            var products =
                mapper.Map<IEnumerable<IndexProductViewModel>>(productsDto);
            return products;
        }

        public static DetailsProductViewModel GetDetailsProductViewModel(CreateProductViewModel createProduct)
        {
            return mapper.Map<DetailsProductViewModel>(createProduct);
        }

        public static DetailsProductViewModel GetDetailsProductViewModel(IndexProductViewModel product)
        {
            return mapper.Map<DetailsProductViewModel>(product);
        }

        public static ModifyProductViewModel GetModifyProductViewModel(IndexProductViewModel product)
        {
            return mapper.Map<ModifyProductViewModel>(product);
        }

        public static ProductDto GetProductDto(CreateProductViewModel client)
        {
            return mapper.Map<ProductDto>(client);
        }
        public static ProductDto GetProductDto(DeleteProductViewModel client)
        {
            return mapper.Map<ProductDto>(client);
        }
        public static ProductDto GetProductDto(ModifyProductViewModel client)
        {
            return mapper.Map<ProductDto>(client);
        }
        public static DeleteProductViewModel GetDeleteProductViewModel(IndexProductViewModel product)
        {
            var entity = mapper.Map<DeleteProductViewModel>(product);
            return entity;
        }
    }
}