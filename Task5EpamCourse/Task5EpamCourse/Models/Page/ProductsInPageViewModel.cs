using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task5EpamCourse.Models.Product;

namespace Task5EpamCourse.Models.Page
{
    public class ProductsInPageViewModel
    {
        public IEnumerable<IndexProductViewModel> Products { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}