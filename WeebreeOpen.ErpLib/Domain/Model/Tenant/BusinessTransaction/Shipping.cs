namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.BusinessTransaction
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Base;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;

    public partial class Shipping : Transaction
    {
        #region Constructors
        
        protected Shipping()
        {
        }
        
        #endregion
        
        #region Key
        
        [Key]
        public int ShippingPkId { get; set; }
        
        #endregion
        
        #region References
        
        #endregion
        
        #region Reference Receiver Location
        
        public Location BusinessPartyReceiverLocationShipping { get; set; }
        
        public StreetAddress BusinessPartyReceiverStreetAddressShipping { get; set; }
        
        #endregion
        
        #region Properties

        #endregion
    }
}