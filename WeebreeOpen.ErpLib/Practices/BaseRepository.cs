namespace WeebreeOpen.ErpLib.Practices
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public abstract partial class BaseRepository<TEntity> where TEntity : class
    {
        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException();
            }

            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<TEntity>();
        }

        private readonly DbContext dbContext;
        private readonly DbSet<TEntity> dbSet;

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = this.dbSet.Find(id);
            this.Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (this.dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbSet.Attach(entityToDelete);
            }
            this.dbSet.Remove(entityToDelete);
        }

        //------------------------------------------------------------------------------------------------------------
        // TODO: 2013-06-27/hp: Ich glaube, diese Get() Funktion sollte nicht freigegeben werden, ansonsten werden
        //                      bestimmte Dinge irgendwo im Code erscheinen und nicht zentrall im Repository.
        //------------------------------------------------------------------------------------------------------------
        //public virtual IEnumerable<TEntity> Get(
        //    Expression<Func<TEntity, bool>> filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        //    string includeProperties = "")
        //{
        //    IQueryable<TEntity> query = this.dbSet;
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }
        //    foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //    {
        //        query = query.Include(includeProperty);
        //    }
        //    if (orderBy != null)
        //    {
        //        return orderBy(query).ToList();
        //    }
        //    else
        //    {
        //        return query.ToList();
        //    }
        //}
        public virtual IQueryable<TEntity> GetAll()
        {
            return this.dbSet.AsQueryable();
        }

        public virtual TEntity GetById(int id)
        {
            return this.dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            this.dbSet.Attach(entityToUpdate);
            this.dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}