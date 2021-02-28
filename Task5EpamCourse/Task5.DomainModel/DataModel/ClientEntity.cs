using System.Collections.Generic;

namespace Task5.DomainModel.DataModel
{
    public class ClientEntity
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public string ClientTelephone { get; set; }

        public virtual ICollection<PurchaseEntity> Purchases { get; set; }

        public ClientEntity()
        {
            Purchases = new List<PurchaseEntity>();
        }
    }
}