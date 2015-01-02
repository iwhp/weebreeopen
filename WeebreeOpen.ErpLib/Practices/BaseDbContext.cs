namespace WeebreeOpen.ErpLib.Practices
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Security.Principal;

    public abstract partial class BaseDbContext : DbContext, IDisposable
    {
        public BaseUnitOfWork<BaseDbContext> UnitOfWork { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="BaseDbContext"/>.
        /// </summary>
        /// <param name="connectionString">Connection string which will be used by creating a <see cref="BaseDbContext"/>.</param>
        /// <param name="currentUser">Username which will set in RecordModifiedBy during save, if specified, otherwhise WindowsIdentity.GetCurrent().Name is used.</param>
        public BaseDbContext(string nameOrConnectionString, string currentUsername = "")
            : base(nameOrConnectionString)
        {
            Database.SetInitializer<BaseDbContext>(null);

            this.CurrentUsername = currentUsername;
            this.ConfigureDbContext();
        }

        public ObjectContext ObjectContext
        {
            get
            {
                return ((IObjectContextAdapter)this).ObjectContext;
            }
        }

        /// <summary>
        /// During an entity update, SaveChanges will write the RecordModifiedBy property.
        /// If CurrentUsername is set, this value will be used.
        /// If CurrentUser is empty, the WindowsIdentity.GetCurrent().Name is used.
        /// </summary>
        /// <value>The current username.</value>
        public string CurrentUsername { get; set; }

        public override int SaveChanges()
        {
            this.OnSaveChanges();
            this.SetBaseEntity_RecordModified();
            return base.SaveChanges();
        }

        protected void ConfigureDbContext()
        {
            // Do NOT enable proxied entities, else serialization fails
            this.Configuration.ProxyCreationEnabled = false;
            // Load navigation properties explicitly (avoid serialization trouble)
            this.Configuration.LazyLoadingEnabled = false;
            // Because Web API will perform validation, we don't need/want EF to do so
            //this.Configuration.ValidateOnSaveEnabled = false;
            //DbContext.Configuration.AutoDetectChangesEnabled = false;
            // We won't use this performance tweak because we don't need 
            // the extra performance and, when autodetect is false,
            // we'd have to be careful. We're not being that careful.
        }

        private void SetBaseEntity_RecordModified()
        {
            ObjectContext context = this.ObjectContext;

            string recordModifiedBy = this.CurrentUsername;
            if (string.IsNullOrWhiteSpace(this.CurrentUsername))
            {
                recordModifiedBy = WindowsIdentity.GetCurrent().Name;
            }
            DateTimeOffset recordModifiedAt = DateTimeOffset.Now;

            //var i1 = context.ObjectStateManager.GetObjectStateEntries(EntityState.Added);
            //var i2 = context.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted);
            ////var i3 = context.ObjectStateManager.GetObjectStateEntries(EntityState.Detached);
            //var i4 = context.ObjectStateManager.GetObjectStateEntries(EntityState.Modified);
            //var i5 = context.ObjectStateManager.GetObjectStateEntries(EntityState.Unchanged);

            foreach (ObjectStateEntry entry in context.ObjectStateManager
                                                      .GetObjectStateEntries(EntityState.Added | EntityState.Modified)
                                                      .Where(x => x.Entity is BaseEntity))
            {
                BaseEntity entityBase = entry.Entity as BaseEntity;
                //entityBase.RecordModifiedAt = recordModifiedAt;
                //entityBase.RecordModifiedBy = recordModifiedBy;
            }
        }

        public virtual void OnSaveChanges()
        {
        }

        public void RollBack()
        {
            ObjectContext context = this.ObjectContext;

            var changedEntries = context.ObjectStateManager.GetObjectStateEntries(EntityState.Added | EntityState.Modified | EntityState.Deleted);

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.ChangeState(EntityState.Unchanged);
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.ChangeState(EntityState.Detached);
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.ChangeState(EntityState.Unchanged);
                entry.CurrentValues.SetValues(entry.OriginalValues); // I am not sure if this one is needed
            }
        }

        #region IDisposable

        new public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        new protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                //this.Dispose();
            }
        }
        
        #endregion
    }
}