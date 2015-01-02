namespace WeebreeOpen.ErpLib.Practices
{
    using System;
    using System.Linq;

    public abstract partial class BaseEntityTenant : BaseEntity
    {
        public BaseEntityTenant()
        {
        }

        /// <summary>
        /// Gets or sets the TenantId.
        /// </summary>
        /// <remarks>
        /// A Tenant is a company or a legal entity for which this system manages ERP data.
        /// Different Root Tenants cannot reference data.
        /// Child Tenants can reference data.
        /// </remarks>
        public int TenantId { get; set; }
    }
}