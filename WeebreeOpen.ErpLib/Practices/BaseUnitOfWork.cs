namespace WeebreeOpen.ErpLib.Practices
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Transactions;

    public abstract partial class BaseUnitOfWork<TDbContext>
    {
        public BaseUnitOfWork(TDbContext dbContext, string nameOrConnectionString, string currentUsername = "")
        {
            this.DbContext = dbContext;
            //var x = this.DbContext as BaseDbContext;
            //x.UnitOfWork = this as BaseUnitOfWork<BaseDbContext>;
            this.nameOrConnectionString = nameOrConnectionString;
            this.currentUsername = currentUsername;
        }

        protected readonly string currentUsername;
        protected readonly string nameOrConnectionString;

        public TDbContext DbContext { get; set; }
        
        public virtual int OnSaveChanges()
        {
            return 0;
        }

        public void Rollback()
        {
            (this.DbContext as BaseDbContext).RollBack();
        }

        public int SaveChanges()
        {
            int count = 0;
            using (TransactionScope transaction = new TransactionScope())
            {
                count += (this.DbContext as DbContext).SaveChanges();
                count += this.OnSaveChanges();

                transaction.Complete();
            }
            return count;
        }
    }
}