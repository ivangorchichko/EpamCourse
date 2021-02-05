using System.Data.Entity.ModelConfiguration;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.DbContext.Configuration
{
    public class ClientEntityConfiguration : EntityTypeConfiguration<ClientEntity>
    {
        public ClientEntityConfiguration()
        {
            ToTable("dbo.Client");

            HasKey(c => c.Id);
        }
    }
}
