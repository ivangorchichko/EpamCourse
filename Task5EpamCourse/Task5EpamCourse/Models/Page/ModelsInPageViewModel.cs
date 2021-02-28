using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.Models.Page
{
    public class ModelsInPageViewModel
    {
        public IEnumerable<ClientViewModel> Clients { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<PurchaseViewModel> Purchases { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}