using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Task5.BL.Contacts;
using Task5.BL.Models;
using Task5EpamCourse.MapperWebConfig;
using Task5EpamCourse.Models.Manager;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;
using Task5EpamCourse.Service.Contracts;

namespace Task5EpamCourse.Service
{
    public class ProductMapper : IProductMapper
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductMapper(IProductService productService)
        {
            _productService = productService;

            _mapper = new Mapper(AutoMapperWebConfig.Configure());
        }

        public ProductViewModel GetProductViewModel(int? id)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(_productService.GetProductsDto())
                    .ToList().Find(x => x.Id == id);
        }
        public IEnumerable<ProductViewModel> GetProductViewModel()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(_productService.GetProductsDto())
                .OrderBy(d => d.Date);
        }


        public ProductDto GetProductDto(ProductViewModel productViewModel)
        {
            return _mapper.Map<ProductDto>(productViewModel);
        }

    }
}