using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.UnitOfWork.Contacts
{
    public interface IUnitOfWork
    {
        void SaveContext();
        void Dispose();
        void AddNewPurchase(ClientEntity client, ProductEntity product, DateTime date);
    }
}
