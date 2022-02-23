using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Electra_WebApi
{
    public interface IntEntityRepository<TEntity>
    {
        IEnumerable<TEntity> Get(
                  Expression<Func<TEntity, bool>> filter = null,
                  Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                  string includeProperties = "");

        TEntity GetByID(object id);

        IQueryable<TEntity> GetAllList();

        void Insert(TEntity entity);

        void InsertBulk(List<TEntity> entity);

        void Delete(int id);

        void Delete(TEntity entityToDelete);

        void DeleteBulk(List<TEntity> entityToDelete);

        void Update(TEntity entityToUpdate);

        void TruncateTable(string tableName);

        int GetTotalCount();
    }
}