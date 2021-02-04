using System.Data.Entity.ModelConfiguration;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext.Configuration
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<ProductEntity>
    {
        public ProductEntityConfiguration()
        {
            this.ToTable("dbo.Product");

            this.HasKey(p => p.Id);

        }
    }
}
