using System.Data.Entity.ModelConfiguration;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext.Configuration
{
    public class PurchaseEntityConfiguration : EntityTypeConfiguration<PurchaseEntity>
    {
        public PurchaseEntityConfiguration()
        {
            this.ToTable("dbo.Purchase");

            this.HasKey(p => p.Id);

            this.HasRequired(p => p.Client)
                .WithRequiredPrincipal(c => c.Purchase);

            this.HasRequired(pro => pro.Product)
                .WithRequiredPrincipal(pur => pur.Purchase);
        }
    }
}
