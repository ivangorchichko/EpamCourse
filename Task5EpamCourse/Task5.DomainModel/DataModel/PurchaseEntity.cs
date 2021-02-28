using System;

namespace Task5.DomainModel.DataModel
{
    public class PurchaseEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ClientId { get; set; }

        public virtual ClientEntity Client { get; set; }

        public int ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }
    }
}