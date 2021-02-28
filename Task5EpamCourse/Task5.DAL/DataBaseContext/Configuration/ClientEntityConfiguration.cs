using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.DomainModel.DataModel;

namespace Task5.DAL.DataBaseContext.Configuration
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
