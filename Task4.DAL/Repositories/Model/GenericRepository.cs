using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task4.DAL.Repositories.Contracts;
using Task4.DAL.DbContext;

namespace Task4.DAL.Repositories.Model
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly PurchaseContext _context;
        private readonly DbSet<TEntity> _entities;

        public GenericRepository(PurchaseContext context)
        {
            this._context = context;
            this._entities = context.Set<TEntity>();
        }
        public IQueryable<TEntity> Get()
        {
            return _entities;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Add(entity);
        }

        public void Remove(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _entities.Remove(entity);
        }

        public void Attach(TEntity entity)
        {
            _entities.Attach(entity);
        }

        public void Reload(TEntity entity)
        {
            _context.Entry<TEntity>(entity).Reload();
        }
    }
}
