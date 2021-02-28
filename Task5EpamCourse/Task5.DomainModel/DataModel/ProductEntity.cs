using System;
using System.Collections.Generic;
using Task5.DomainModel.Contract;

namespace Task5.DomainModel.DataModel
{
    public class ProductEntity : IGenericProperty
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<PurchaseEntity> Purchases { get; set; }

        public ProductEntity()
        {
            Purchases = new List<PurchaseEntity>();
        }
    }
}