using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Enums;
using Task5.BL.Models;

namespace Task5.BL.Contacts
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProductDto();
        void AddProduct(ProductDto productDto);
        void ModifyProduct(ProductDto productDto);
        void RemoveProduct(ProductDto productDto);
        IEnumerable<ProductDto> GetFilteredProductDto(TextFieldFilter filter, string fieldString);
    }
}
