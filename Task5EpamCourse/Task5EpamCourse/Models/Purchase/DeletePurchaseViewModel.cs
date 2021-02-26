using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task5EpamCourse.Models.Purchase
{
    public class DeletePurchaseViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string ClientName { get; set; }

        public string ClientTelephone { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }
    }
}