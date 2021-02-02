using System;
using System.Linq;
using System.Linq.Expressions;

namespace Task4.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get();

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Attach(TEntity entity);

        void Reload(TEntity entity);
    }
}
