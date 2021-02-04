using System;
using System.Linq;
using System.Linq.Expressions;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.DbContext;

namespace Task4.DAL.Repositories.Model
{
    public class Repository : IRepository
    {
        public Repository(PurchaseContext context)
        {
            _context = context;
        }

        private readonly PurchaseContext _context;

        public IQueryable<TEntity> Get<TEntity>() 
            where TEntity: class
        {
            return _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Add(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _context.Set<TEntity>().Remove(entity);
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Reload<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).Reload();
        }
    }
}
