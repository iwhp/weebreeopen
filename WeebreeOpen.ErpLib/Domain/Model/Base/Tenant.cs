namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/ADClient
    /// </summary>
    public partial class Tenant : BaseEntity
    {
        /// <summary>
        /// A Tenant is a company or a legal entity for which this system manages ERP data.
        /// Different Root Tenants cannot reference data.
        /// Child Tenants can reference data.
        /// </summary>
        public Tenant()
        {
        }

        public string TenantName { get; set; }

        /// <summary>
        /// Gets or sets the TenantParentId.
        /// </summary>
        /// <remarks>
        /// A Root Tenant does not have the TenantParentId set, it is null.
        /// </remarks>
        public int TenantParentId { get; set; }

        /// <summary>
        /// Gets or sets the TenantPkId.
        /// </summary>
        public int TenantPkId { get; set; }
    }
}