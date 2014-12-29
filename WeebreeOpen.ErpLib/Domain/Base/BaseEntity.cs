namespace WeebreeOpen.ErpLib.Domain.Base
{
    using System;
    using System.Linq;

    public class BaseEntity
    {
        public BaseEntity()
        {
            this.IsActive = true;
        }

        /// <summary>
        /// Gets or sets the ClientId.
        /// </summary>
        /// <remarks>
        /// A Client is a company or a legal entity. You cannot share data between Clients.
        /// </remarks>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the ChildUnitId.
        /// </summary>
        /// <remarks>
        /// A unit of your client or legal entity - examples are store, department. You can share data between client units.
        /// </remarks>
        public int ChildUnitId { get; set; }

        /// <summary>
        /// Gets or sets IsActive code.
        /// </summary>
        /// <remarks>
        /// There are two methods of making records unavailable in the system: One is to delete the record, the other is to de-activate the record.
        /// A de-activated record is not available for selection, but available for reporting.
        /// There are two reasons for de-activating and not deleting records:
        /// (1) The system requires the record for auditing purposes.
        /// (2) The record is referenced by other records. E.g., you cannot delete a Business Partner, if there are existing invoices for it.
        /// By de-activating the Business Partner you prevent it from being used in future transactions.
        /// </remarks>
        /// <value>true if record is acctive, false if record is not active.</value>
        public bool IsActive { get; set; }
    }
}
