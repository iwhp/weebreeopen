namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.BusinessTransaction
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Base;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Order
    ///
    /// This is either a Sales Order or a Purchase Order, depending of who is the viewer (sender or receiver).
    /// </summary>
    public partial class Order : Transaction
    {
        #region Constructors
        
        protected Order()
        {
        }
        
        #endregion
        
        #region Key
        
        [Key]
        public int OrderPkId { get; set; }
        
        #endregion
        
        #region References
        
        #endregion
        
        #region Reference Receiver Location
        
        public Location BusinessPartyReceiverLocationInvoice { get; set; }
        
        public StreetAddress BusinessPartyReceiverStreetAddressInvoice { get; set; }
        
        #endregion
        
        #region Properties

        #endregion
    }
}