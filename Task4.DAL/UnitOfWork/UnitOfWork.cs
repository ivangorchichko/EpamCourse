using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.Repositories.Model;
using Task4.DAL.UnitOfWork.Contacts;

namespace Task4.DAL.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private IRepository _repository;
        private readonly PurchaseContext _context = new PurchaseContext();
        private bool _disposed;

        public IRepository Repository
        {
            get
            {
                if (_repository != null)
                {
                    return _repository;
                }
                else
                {
                   return _repository = new Repository(_context);
                }
            }
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
