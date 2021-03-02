using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Models;
using Task5EpamCourse.Models.Product;

namespace Task5EpamCourse.Service.Contracts
{
    public interface IProductMapper
    {
        ProductViewModel GetProductViewModel(int? id);

        IEnumerable<ProductViewModel> GetProductViewModel();

        ProductDto GetProductDto(ProductViewModel productViewModel);
    }
}
