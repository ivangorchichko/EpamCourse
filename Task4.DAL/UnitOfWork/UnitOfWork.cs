using System;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;
using Task4.DAL.UnitOfWork.Contacts;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.UnitOfWork
{
    public sealed class UnitOfWork: IUnitOfWork 
    {
        public UnitOfWork(PurchaseContext context)
        {
            _context = context;
            _purchaseRepository = new Repository(context);
        }

        private readonly IRepository _purchaseRepository;
        private readonly PurchaseContext _context;
        private bool _disposed;

        public void AddNewPurchase(ClientEntity client, ProductEntity product, DateTime date)
        {
            var purchase = new PurchaseEntity
            {
                Client = client,
                Product = product,
                Date = date,
            };
            purchase.Client.PurchaseId = purchase.Id;
            purchase.Product.PurchaseId = purchase.Id;
            purchase.ClientId = purchase.Client.Id;
            purchase.ProductId = purchase.Product.Id;
            _purchaseRepository.Add<PurchaseEntity>(purchase);
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
    }
}
