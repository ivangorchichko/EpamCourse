using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task5.DomainModel.Contract;

namespace Task5.DAL.Repository.Contract
{
    public interface IRepository
    {
        IEnumerable<TEntity> Get<TEntity>() where TEntity : class;

        IEnumerable<TEntity> Get<TEntity>(int pageSize, int page, Expression<Func<TEntity, bool>> predicate = null) where TEntity : class, IGenericProperty;

        void Add<TEntity>(TEntity entity) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        void Save();

    }
}
