namespace WeebreeOpen.ErpLib.Domain.Common
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Base;

    // http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/BusinessPartnerLocation

    /// <summary>
    /// The Location defines the physical location of a business partner. A business partner may have multiple location records.
    /// </summary>
    public class BusinessPartyLocation : BaseEntity
    {
        public int BusinessPartyLocationPkId { get; set; }

        public int BusinessPartyId { get; set; }

        public int LocationId { get; set; }

        /// <summary>
        /// This fieldindicates that this location is a fiscal address. Fiscal addresses are shown in all documents.
        /// </summary>
        public bool IsTaxLocation { get; set; }

        /// <summary>
        /// The Invoicing Address checkbox indicates if this location is the Invoicing Address for this Business Partner.
        /// </summary>
        public bool IsInvoiceToAddress { get; set; }

        /// <summary>
        /// The Shipping Address checkbox indicates if this location is the address to use when shipping orders to this Business Partner.
        /// </summary>
        public bool IsShipToAddress { get; set; }

        /// <summary>
        /// The Pay From Address checkbox indicates if this location is the address the Business Partner pays from.
        /// </summary>
        public bool IsPayFromAddress { get; set; }

        /// <summary>
        /// The Remit to Address checkbox indicates if this location is the address to which we should send payments to this Business Partner.
        /// </summary>
        public bool IsRemitToAddress { get; set; }
    }
}
