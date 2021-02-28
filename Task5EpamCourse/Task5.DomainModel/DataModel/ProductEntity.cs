using System.Collections.Generic;

namespace Task5.DomainModel.DataModel
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public virtual ICollection<PurchaseEntity> Purchases { get; set; }

        public ProductEntity()
        {
            Purchases = new List<PurchaseEntity>();
        }
    }
}