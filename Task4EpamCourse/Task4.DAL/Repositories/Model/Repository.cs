using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Task4.DAL.DbContext;
using Task4.DAL.Repositories.Contracts;

using Context = System.Data.Entity.DbContext;

namespace Task4.DAL.Repositories.Model
{
    public class Repository : IRepository
    {
        public Repository(Context context)
        {
            _context = context;
        }

        private readonly Context _context;

        public IEnumerable<TEntity> Get<TEntity>() 
            where TEntity: class
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
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
        }

        public void Reload<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).Reload();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
