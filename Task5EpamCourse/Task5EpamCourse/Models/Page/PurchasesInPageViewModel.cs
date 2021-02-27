using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task5EpamCourse.Models.Purchase;

namespace Task5EpamCourse.Models.Page
{
    public class PurchasesInPageViewModel
    {
        public IEnumerable<IndexPurchaseViewModel> Purchases { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}