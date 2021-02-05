using System.Data.Entity;
using Task4.DAL.DbContext.Configuration;

using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext
{
    public class PurchaseContext: System.Data.Entity.DbContext
    {
        public PurchaseContext() : base("Sale")
        {
             Database.SetInitializer(new MigrateDatabaseToLatestVersion<PurchaseContext, Migrations.Configuration>());
        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<PurchaseEntity> Purchases { get; set; }

        public DbSet<ProductEntity> Products { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PurchaseEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new ClientEntityConfiguration());
        }
    }
}
