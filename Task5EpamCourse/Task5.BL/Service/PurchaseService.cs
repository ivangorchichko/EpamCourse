using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Enums;
using Task5.BL.MapperBLHelper;
using Task5.BL.Models;
using Task5.DAL.Repository.Contract;
using Task5.DAL.Repository.Model;
using Task5.DomainModel.DataModel;

namespace Task5.BL.Service
{
    public class PurchaseService : IPurchaseService
    {
        private bool _disposed = false;
        private IMapper _mapper;
        private IRepository _repository;

        public PurchaseService(IRepository repository)
        {
            //запихнуть в старттап
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }
        public IEnumerable<PurchaseDto> GetPurchaseDto(int page, Expression<Func<PurchaseEntity, bool>> predicate = null)
        {
            var purchasesEntities = new List<PurchaseEntity>();
            if (predicate != null)
            {
                purchasesEntities = _repository.Get<PurchaseEntity>(3, page, predicate).ToList();
            }
            else
            {
                purchasesEntities = _repository.Get<PurchaseEntity>(3, page).ToList();
            }

            var purchases =
                _mapper.Map<IEnumerable<PurchaseDto>>(purchasesEntities);
            return purchases;
        }
        public IEnumerable<PurchaseDto> GetPurchaseDto()
        {
            var purchasesEntities = _repository.Get<PurchaseEntity>();
            var clientEntities = _repository.Get<ClientEntity>();
            var productEntities = _repository.Get<ProductEntity>();
            var managerEntities = _repository.Get<ManagerEntity>();
            var manager 
                = _mapper.Map<IEnumerable<ManagerDto>>(managerEntities);
            var client =
                _mapper.Map<IEnumerable<ClientDto>>(clientEntities);
            var product =
                _mapper.Map<IEnumerable<ProductDto>>(productEntities);
            var purchases =
                _mapper.Map<IEnumerable<PurchaseDto>>(purchasesEntities);
            return purchases;
        }

        public void AddPurchase(PurchaseDto purchaseDto, string selectManager)
        {
            purchaseDto.Id = GetPurchaseDto().ToList().Count;
            var client = _repository.Get<ClientEntity>()
                .FirstOrDefault(
                    c => c.ClientName == purchaseDto.Client.ClientName &&
                    c.ClientTelephone == purchaseDto.Client.ClientTelephone);
            var product = _repository.Get<ProductEntity>()
                .FirstOrDefault(
                    p => p.ProductName == purchaseDto.Product.ProductName &&
                    p.Price == purchaseDto.Product.Price);
            var manager = _repository.Get<ManagerEntity>()
                .FirstOrDefault(m => m.ManagerName == selectManager
                );
            var purchase = _mapper.Map<PurchaseEntity>(purchaseDto);
            if (client != null && product != null && manager != null)
            {
                purchase.Client = client;
                purchase.Product = product;
                purchase.Manager = manager;
            }else
            if (client != null)
            {
                purchase.Client = client;
            }
            if (product != null)
            {
                purchase.Product = product;
            }
            if (manager != null)
            {
                purchase.Manager = manager;
            }

            _repository.Add<PurchaseEntity>(purchase);
        }

        public void ModifyPurchase(PurchaseDto purchaseDto)
        {
            var purchaseEntity = _repository.Get<PurchaseEntity>().ToList()
                .Find(purchase => purchase.Id == purchaseDto.Id);
            purchaseEntity.Product.ProductName = purchaseDto.Product.ProductName;
            purchaseEntity.Product.Price = purchaseDto.Product.Price;
            purchaseEntity.Product.Date = purchaseDto.Product.Date;
            purchaseEntity.Client.ClientName = purchaseDto.Client.ClientName;
            purchaseEntity.Client.ClientTelephone = purchaseDto.Client.ClientTelephone;
            purchaseEntity.Client.Date = purchaseDto.Client.Date;
            purchaseEntity.Manager.ManagerName = purchaseDto.Manager.ManagerName;
            purchaseEntity.Manager.ManagerTelephone = purchaseDto.Manager.ManagerTelephone;
            purchaseEntity.Manager.ManagerRank = purchaseDto.Manager.ManagerRank;
            purchaseEntity.Manager.Date = purchaseDto.Manager.Date;
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

        public IEnumerable<PurchaseDto> GetFilteredPurchaseDto(TextFieldFilter filter, string fieldString, int page)
        {
            var purchase = new PurchaseDto();
            var purchases = new List<PurchaseDto>();
            switch (filter)
            {
                case TextFieldFilter.ClientName:
                {
                    purchases = GetPurchaseDto(page, p => purchase.Client.ClientName == fieldString).ToList();
                     //   purchases = purchases.Where(purchase => purchase.Client.ClientName == fieldString).ToList();
                    break;
                }
                case TextFieldFilter.ProductName:
                {
                   // purchases = GetPurchaseDto(page);
                    //    purchases = purchases.Where(purchase => purchase.Product.ProductName == fieldString).ToList();
                    break;
                }
                case TextFieldFilter.Date:
                {
                   // purchases = GetPurchaseDto(page);
                    //    purchases = purchases.Where(purchase => purchase.Date.ToString().Contains(fieldString)).ToList();
                    break;
                }
            }

            return purchases;
        }
    }
}