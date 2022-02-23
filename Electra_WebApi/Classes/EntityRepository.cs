using EntityClass;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Electra_WebApi
{

    public class EntityRepository<TEntity> : IntEntityRepository<TEntity> where TEntity : class
    {
        internal CraModel context;
        internal DbSet<TEntity> dbSet;

        public EntityRepository(CraModel _context)
        {
            this.context = _context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (string includeProperty in includeProperties.Split
                (new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            return orderBy != null ? orderBy(query).ToList() : query.ToList();
        }


        public virtual IQueryable<TEntity> GetAllList()
        {
            return dbSet.AsQueryable();
        }


        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }


        public virtual void InsertBulk(List<TEntity> entity)
        {
            //EFBatchOperation.For(context, dbSet).InsertAll(entity);
        }


        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }


        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == System.Data.Entity.EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);
        }


        public virtual void DeleteBulk(List<TEntity> entityToDelete)
        {
            dbSet.RemoveRange(entityToDelete);
            context.SaveChanges();
        }


        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public virtual void UpdateBulk(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = System.Data.Entity.EntityState.Modified;
        }


        public virtual void TruncateTable(string tableName)
        {
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE " + tableName);
        }


        public virtual int GetTotalCount()
        {
            return dbSet.Count();
        }
    }
}