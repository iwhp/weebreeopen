namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Product
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Base;
    using WeebreeOpen.ErpLib.Practices;

    public partial class ItemPrice : BaseEntityTenant
    {
        #region Constructors

        protected ItemPrice()
        {
        }

        public ItemPrice(int itemId, decimal unitPrice, string unitOfMeasure)
        {
            if (string.IsNullOrWhiteSpace(unitOfMeasure))
            {
                throw (new ArgumentNullException("unitOfMeasure"));
            }

            this.ItemId = itemId;
            this.UnitPrice = unitPrice;
            this.UnitOfMeasure = unitOfMeasure;
        }

        #endregion

        #region Key

        [Key]
        public int ItemPricePkId { get; set; }

        #endregion

        #region References

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        #endregion

        #region Properties

        public decimal UnitPrice { get; set; }

        public string UnitOfMeasure { get; set; }

        public int QuantityMinimum { get; set; }

        public DateTimeOffset DateTimeValidFrom { get; set; }

        public DateTimeOffset DateTimeVaildTo { get; set; }

        #endregion
    }
}