using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task5.BL.Enums;

namespace Task5.BL.Models
{
    public class PurchaseDto 
    {
        public int Id { get; set; }

        public ClientDto Client { get; set; }

        public ProductDto Product { get; set; }

        public DateTime Date { get; set; }
    }
}
