using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Enums;
using Task5.BL.MapperBLHelper;
using Task5.BL.Models;
using Task5.DAL.Repository.Contract;
using Task5.DomainModel.DataModel;

namespace Task5.BL.Service
{
    public class ProductService : IProductService
    {
        private static IMapper _mapper;
        private static IRepository _repository;

        public ProductService(IRepository repository)
        {
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }

        public IEnumerable<ProductDto> GetProductDto()
        {
            var productEntities = _repository.Get<ProductEntity>();
            var products =
                _mapper.Map<IEnumerable<ProductDto>>(productEntities);
            return products;
        }

        public void AddProduct(ProductDto productDto)
        {
            _repository.Add<ProductEntity>(_mapper.Map<ProductEntity>(productDto));
        }

        public void ModifyProduct(ProductDto productDto)
        {
            var productEntity = _repository.Get<ProductEntity>().ToList()
                .Find(product => product.Id == productDto.Id);
            productEntity.ProductName = productDto.ProductName;
            productEntity.Price = productDto.Price;
            productEntity.Id = productDto.Id;
            _repository.Save();
        }

        public void RemoveProduct(ProductDto productDto)
        {
            var productEntity = _repository.Get<ProductEntity>()
                .ToList()
                .Find(product => product.Id == productDto.Id);
            _repository.Remove(productEntity);
        }

        public IEnumerable<ProductDto> GetFilteredProductDto(TextFieldFilter filter, string fieldString)
        {
            var products = GetProductDto();
            switch (filter)
            {
                case TextFieldFilter.ProductName:
                    {
                        products = products.Where(product => product.ProductName == fieldString).ToList();
                        break;
                    }
                case TextFieldFilter.Price:
                    {
                        products = products.Where(product => product.Price == Double.Parse(fieldString)).ToList();
                        break;
                    }
            }
            return products;
        }
    }
}
