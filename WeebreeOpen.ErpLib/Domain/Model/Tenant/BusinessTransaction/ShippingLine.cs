namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.BusinessTransaction
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public partial class ShippingLine : TransactionLine
    {
        #region Constructors
        
        protected ShippingLine()
        {
        }
        
        #endregion
        
        #region Key
        
        [Key]
        public int ShippingLinePkId { get; set; }
        
        #endregion
        
        #region References
        
        public int ShippingId { get; set; }
        
        public Shipping Shipping { get; set; }
        
        #endregion
        
        #region Properties

        #endregion
    }
}