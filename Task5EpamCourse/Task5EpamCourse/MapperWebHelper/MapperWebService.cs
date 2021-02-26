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
        public static PurchaseDto GetPurchaseDto(IndexPurchaseViewModel purchase)
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
    }
}