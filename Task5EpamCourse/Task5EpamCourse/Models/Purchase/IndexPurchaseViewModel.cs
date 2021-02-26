using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task5EpamCourse.Models.Client;
using Task5EpamCourse.Models.Product;

namespace Task5EpamCourse.Models.Purchase
{
    public class IndexPurchaseViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public IndexClientViewModel Client { get; set; }

        public IndexProductViewModel Product { get; set; }
    }
}