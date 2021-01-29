using System.Data.Entity;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.Repositories.Context
{
    public class PurchaseContext: DbContext
    {
        public PurchaseContext() : base()
        {

        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<PurchaseEntity> Purchases { get; set; }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
