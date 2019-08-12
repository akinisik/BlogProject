using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Blog.Core.Infrastructure
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate = null, string includes = null);
        TEntity GetById(object id);
        TEntity GetSingle(Expression<Func<TEntity, bool>> predicate, string includes = null);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        void MultipleInsert(IEnumerable<TEntity> entities);
    }
}
