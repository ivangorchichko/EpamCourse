using System.Data.Entity;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext
{
    public class PurchaseContext: System.Data.Entity.DbContext
    {
        public PurchaseContext() : base("SalesDataModelContainer")
        {

        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<PurchaseEntity> Purchases { get; set; }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
