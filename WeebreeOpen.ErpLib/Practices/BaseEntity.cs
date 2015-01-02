namespace WeebreeOpen.ErpLib.Practices
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public abstract partial class BaseEntity
    {
        public BaseEntity()
        {
            this.IsActive = true;
        }

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
        /// <value>true if record is active, false if record is de-activated.</value>
        public bool IsActive { get; set; }

        [Timestamp]
        public byte[] RecordTimestamp { get; set; }
    }
}