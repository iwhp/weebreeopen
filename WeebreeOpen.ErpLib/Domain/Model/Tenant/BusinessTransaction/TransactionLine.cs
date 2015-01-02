namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.BusinessTransaction
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Product;
    using WeebreeOpen.ErpLib.Practices;

    public partial class TransactionLine : BaseEntityTenant
    {
        #region Constructors
        
        #endregion
        
        #region Key

        [Key]
        public int TransactionLinePkId { get; set; }
        
        #endregion
        
        #region References
        
        public int OrderId { get; set; }
        
        public Order Order { get; set; }
        
        #endregion
        
        #region Reference Item
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
            
        public string ItemName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.itemName))
                {
                    return this.Item.Name;
                }
                return this.itemName;
            }
            set
            {
                this.itemName = value;
            }
        }
        
        private string itemName;
        
        #endregion
        
        #region Properties
    
        public decimal QuantityUnitOfMeasure { get; set; }

        #endregion
    }
}