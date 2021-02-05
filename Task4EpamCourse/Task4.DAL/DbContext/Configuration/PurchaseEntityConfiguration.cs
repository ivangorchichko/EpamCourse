using System.Data.Entity.ModelConfiguration;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext.Configuration
{
    public class PurchaseEntityConfiguration : EntityTypeConfiguration<PurchaseEntity>
    {
        public PurchaseEntityConfiguration()
        {
            ToTable("dbo.Purchase");

            HasKey(p => p.Id);

            HasRequired(p => p.Client)
                .WithRequiredPrincipal(c => c.Purchase);

            HasRequired(pro => pro.Product)
                .WithRequiredPrincipal(pur => pur.Purchase);
        }
    }
}
