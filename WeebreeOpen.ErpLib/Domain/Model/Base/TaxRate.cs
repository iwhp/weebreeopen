namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/Order
    /// </summary>
    public partial class TaxRate : BaseEntityTenant
    {
        #region Constructors

        protected TaxRate()
        {
        }

        #endregion

        #region Key

        [Key]
        public int TaxRatePkId { get; set; }

        #endregion

        #region References

        public int TaxId { get; set; }

        public Tax Tax { get; set; }

        #endregion

        #region Properties

        public decimal Rate { get; set; }

        public DateTimeOffset DateTimeValidFrom { get; set; }

        public DateTimeOffset DateTimeValidTo { get; set; }

        #endregion
    }
}