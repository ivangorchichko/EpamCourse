using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Task5.BL.Enums;
using Task5.BL.Models;
using Task5.DomainModel.DataModel;

namespace Task5.BL.Contacts
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProductsDto();
        IEnumerable<ProductDto> GetProductsDto(int page, Expression<Func<ProductEntity, bool>> predicate = null);
        void AddProduct(ProductDto productDto);
        void ModifyProduct(ProductDto productDto);
        void RemoveProduct(ProductDto productDto);
        IEnumerable<ProductDto> GetFilteredProductDto(TextFieldFilter filter, string fieldString, int page);
    }
}
