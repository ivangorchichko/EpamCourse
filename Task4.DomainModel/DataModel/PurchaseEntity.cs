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
    public class PurchaseEntity
    {
        [Key]
        public int PurchaseId { get; set; }

        public DateTime Date { get; set; }

        public virtual ClientEntity Client { get; set; }

        public virtual ProductEntity Product { get; set; }

    }
}
