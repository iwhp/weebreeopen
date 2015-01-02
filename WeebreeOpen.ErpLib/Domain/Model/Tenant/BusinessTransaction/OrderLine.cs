namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.BusinessTransaction
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Base;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/OrderLine
    /// </summary>
    public partial class OrderLine : TransactionLine
    {
        #region Constructors
        
        protected OrderLine()
        {
        }
        
        #endregion
        
        #region Key
        
        [Key]
        public int OrderLinePkId { get; set; }
        
        #endregion
        
        #region References
        
        public int TaxId { get; set; }
        
        public Tax Tax { get; set; }
        
        #endregion
        
        #region Properties
        
        public decimal DiscountPercentage { get; set; }
        
        public decimal DiscountAmount { get; set; }
        
        #endregion
    }
}