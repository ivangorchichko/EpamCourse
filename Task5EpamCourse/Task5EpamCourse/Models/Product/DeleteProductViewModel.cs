using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Task5EpamCourse.Models.Product
{
    public class DeleteProductViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }
    }
}