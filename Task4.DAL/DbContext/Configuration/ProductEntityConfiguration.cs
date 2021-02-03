using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext.Configuration
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<ProductEntity>
    {
        public ProductEntityConfiguration()
        {
            this.ToTable("SalesDataModelContainer");

            this.HasKey(p => p.Id);

            this.Property(p => p.Price)
                .HasColumnName("ProductPrice")
                .HasColumnType("double");

            this.Property(p => p.ProductName)
                .HasMaxLength(20);

            this.HasRequired(pr => pr.Purchase)
                .WithRequiredPrincipal(p => p.Product)
                .Map(pr => pr.MapKey("PurchaseId"));
        }
    }
}
