using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;
using Task4.DAL.UnitOfWork.Contacts;
using Task4.DomainModel.DataModel;

namespace Task4.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(PurchaseContext context)
        {
            _context = context;
            _clientRepository = new GenericRepository<ClientEntity>(context);
            _productRepository = new GenericRepository<ProductEntity>(context);
            _purchaseRepository = new GenericRepository<PurchaseEntity>(context);
        }

        private readonly IGenericRepository<ClientEntity> _clientRepository;
        private readonly IGenericRepository<ProductEntity> _productRepository;
        private readonly IGenericRepository<PurchaseEntity> _purchaseRepository;
        private readonly PurchaseContext _context;
        private bool _disposed = false;

        public void AddNewPurchase(ClientEntity client, ProductEntity product, DateTime date)
        {
            var purchase = new PurchaseEntity
            {
                Client = client,
                Product = product,
                Date = date,
            };
            _purchaseRepository.Add(purchase);
        }

        public void SaveContext()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
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

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }
}
