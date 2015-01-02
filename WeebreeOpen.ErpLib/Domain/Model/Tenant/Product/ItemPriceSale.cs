namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Product
{
    using System;
    using System.Linq;

    public partial class ItemPriceSale : ItemPrice
    {
        #region Constructors

        protected ItemPriceSale()
        {
        }

        public ItemPriceSale(int itemId, decimal unitPrice, string unitOfMeasure)
            : base(itemId, unitPrice, unitOfMeasure)
        {
        }

        #endregion

        #region References

        #endregion

        #region Properties

        #endregion
    }
}