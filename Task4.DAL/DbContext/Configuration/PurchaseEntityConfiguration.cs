using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext.Configuration
{
    public class PurchaseEntityConfiguration : EntityTypeConfiguration<PurchaseEntity>
    {
        public PurchaseEntityConfiguration()
        {
            this.ToTable("SalesDataModelContainer");

            this.HasKey(p => p.Id);

            this.Property(p => p.Date)
                .HasColumnName("Data")
                .HasColumnType("datetime2");

            this.HasRequired(p => p.Client)
                .WithRequiredDependent(c => c.Purchase)
                .Map(cp => cp.MapKey("ClientId"));

            this.HasRequired(pro => pro.Product)
                .WithRequiredDependent(pur => pur.Purchase)
                .Map(pp => pp.MapKey("ProductId"));
        }
    }
}
