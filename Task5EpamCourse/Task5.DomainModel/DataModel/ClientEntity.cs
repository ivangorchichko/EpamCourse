using System;
using System.Collections.Generic;
using Task5.DomainModel.Contract;

namespace Task5.DomainModel.DataModel
{
    public class ClientEntity : IGenericProperty
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string ClientTelephone { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<PurchaseEntity> Purchases { get; set; }

        public ClientEntity()
        {
            Purchases = new List<PurchaseEntity>();
        }
    }
}