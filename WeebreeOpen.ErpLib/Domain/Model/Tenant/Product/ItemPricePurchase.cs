namespace WeebreeOpen.ErpLib.Domain.Model.Tenant.Product
{
    using System;
    using System.Linq;
    using WeebreeOpen.ErpLib.Domain.Model.Tenant.Party;

    public partial class ItemPricePurchase : ItemPrice
    {
        #region Constructors

        protected ItemPricePurchase()
        {
        }

        public ItemPricePurchase(int itemId, decimal unitPrice, string unitOfMeasure)
            : base(itemId, unitPrice, unitOfMeasure)
        {
        }

        #endregion

        #region References

        public int BusinessPartyVendorId { get; set; }

        public BusinessPartyVendor BusinessPartyVendor { get; set; }

        #endregion

        #region Properties

        #endregion
    }
}