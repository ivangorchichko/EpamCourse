using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Task5.BL.Enums;
using Task5.BL.MapperBLHelper;
using Task5.BL.Models;
using Task5.DAL.Repository.Contract;
using Task5.DAL.Repository.Model;
using Task5.DomainModel.DataModel;

namespace Task5.BL.Service
{
    public class PurchaseService
    {
        private bool _disposed = false;
        private static IMapper _mapper;
        private static IRepository _repository;

        public PurchaseService(IRepository repository)
        {
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }

        public IEnumerable<PurchaseDto> GetPurchaseDto()
        {
            var purchasesEntities = _repository.Get<PurchaseEntity>();
            var clientEntities = _repository.Get<ClientEntity>();
            var productEntities = _repository.Get<ProductEntity>();
            var client =
                _mapper.Map<IEnumerable<ClientDto>>(clientEntities);
            var product =
                _mapper.Map<IEnumerable<ProductDto>>(productEntities);
            var purchases =
                _mapper.Map<IEnumerable<PurchaseDto>>(purchasesEntities);
            return purchases;
        }

        public void AddPurchase(PurchaseDto purchaseDto)
        {
            _repository.Add<PurchaseEntity>(_mapper.Map<PurchaseEntity>(purchaseDto));
        }

        public void ModifyPurchase(PurchaseDto purchaseDto)
        {
            var purchaseEntity = _repository.Get<PurchaseEntity>().ToList()
                .Find(purchase => purchase.Id == purchaseDto.Id);
            purchaseEntity.Product.ProductName = purchaseDto.Product.ProductName;
            purchaseEntity.Product.Price = purchaseDto.Product.Price;
            purchaseEntity.Client.ClientName = purchaseDto.Client.ClientName;
            purchaseEntity.Client.ClientTelephone = purchaseDto.Client.ClientTelephone;
            purchaseEntity.Date = purchaseDto.Date;
            _repository.Save();
        }

        public void RemovePurchase(PurchaseDto purchaseDto)
        {
            var purchaseEntity = _repository.Get<PurchaseEntity>()
                .ToList()
                .Find(purchase => purchase.Id == purchaseDto.Id);
            _repository.Remove(purchaseEntity);
        }

        public IEnumerable<PurchaseDto> GetFilteresPurchaseDto(TextFieldFilter filter, string fieldString)
        {
            var purchases = GetPurchaseDto();
            switch (filter)
            {
                case TextFieldFilter.ClientName:
                {
                    purchases = purchases.Where(purchase => purchase.Client.ClientName == fieldString).ToList();
                    break;
                }
                case TextFieldFilter.ProductName:
                {
                    purchases = purchases.Where(purchase => purchase.Product.ProductName == fieldString).ToList();
                    break;
                }
                case TextFieldFilter.Date:
                {
                    purchases = purchases.Where(purchase => purchase.Date.ToString().Contains(fieldString)).ToList();
                    break;
                }
            }

            return purchases;
        }
    }
}