using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext.Configuration
{
    public class ClientEntityConfiguration : EntityTypeConfiguration<ClientEntity>
    {
        public ClientEntityConfiguration()
        {
            this.ToTable("SalesDataModelContainer");

            this.HasKey(c => c.Id);

            this.Property(c => c.ClientName)
                .HasMaxLength(20);

            this.HasRequired(c => c.Purchase)
                .WithRequiredPrincipal(p => p.Client)
                .Map(pc => pc.MapKey("PurchaseId"));
        }
    }
}
