using System.Data.Entity;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext
{
    public class PurchaseContext: System.Data.Entity.DbContext
    {
        public PurchaseContext() : base()
        {

        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<PurchaseEntity> Purchases { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseEntity>()
                .HasRequired(s => s.Client)
                .WithRequiredPrincipal(ad => ad.Purchase);
            modelBuilder.Entity<PurchaseEntity>()
                .HasRequired(s => s.Product)
                .WithRequiredPrincipal(ad => ad.Purchase);
            //modelBuilder.Entity<ProductEntity>()
            //    .HasRequired(s => s.Purchase)
            //    .WithRequiredPrincipal(ad => ad.Product);
            //modelBuilder.Entity<ClientEntity>()
            //    .HasRequired(s => s.Purchase)
            //    .WithRequiredPrincipal(ad => ad.Client);
        }
    }
}
