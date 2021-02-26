using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Task5.DAL.DataBaseContext;
using Task5.DAL.Repository.Contract;


namespace Task5.DAL.Repository.Model
{
    public class Repository : IRepository
    {
        public Repository()
        {
            _context = new PurchaseContext();
        }

        private readonly PurchaseContext _context;

        public IEnumerable<TEntity> Get<TEntity>()
            where TEntity : class
        {
            return _context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Add<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
            _context.SaveChanges();
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Reload<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).Reload();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}


