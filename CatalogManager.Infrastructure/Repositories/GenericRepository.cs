using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogManager.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private CatalogManagerContext context;
        private DbSet<TEntity> dbSet;

        public GenericRepository(CatalogManagerContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }


        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }
    }
}
