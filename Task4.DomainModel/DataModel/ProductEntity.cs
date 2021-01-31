using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4.DomainModel.DataModel
{
    public class ProductEntity
    {
        [Key] public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public virtual PurchaseEntity Purchase { get; set; }
    }
}
