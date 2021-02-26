using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.DomainModel.DataModel;

namespace Task5.DAL.DataBaseContext.Configuration
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
