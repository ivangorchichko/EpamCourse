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
    public class ClientEntity
    {
        [Key]
        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public virtual PurchaseEntity Purchase { get; set; }

    }
}
