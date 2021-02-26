using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.DAL.DataBaseContext.Configuration;
using Task5.DomainModel.DataModel;


namespace Task5.DAL.DataBaseContext
{
    public class PurchaseContext : DbContext
    {
        public PurchaseContext() : base("SaleTask5")
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
