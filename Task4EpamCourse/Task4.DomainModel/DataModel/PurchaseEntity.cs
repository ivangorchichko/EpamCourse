using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Task4.DomainModel.DataModel
{
    public class PurchaseEntity
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ClientId { get; set; }

        public ClientEntity Client { get; set; }

        public int ProductId { get; set; }

        public ProductEntity Product { get; set; }

    }
}
