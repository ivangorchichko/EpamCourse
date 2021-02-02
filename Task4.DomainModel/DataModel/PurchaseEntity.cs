using System;

namespace Task4.DomainModel.DataModel
{
    public class PurchaseEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public ClientEntity Client { get; set; }

        public ProductEntity Product { get; set; }

    }
}
