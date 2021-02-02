using System;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.UnitOfWork.Contacts
{
    public interface IUnitOfWork
    {
        void SaveContext();
        void Dispose(bool disposing);
        void AddNewPurchase(ClientEntity client, ProductEntity product, DateTime date);
    }
}
