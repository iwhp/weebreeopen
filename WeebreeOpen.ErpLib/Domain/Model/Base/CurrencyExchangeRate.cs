namespace WeebreeOpen.ErpLib.Domain.Model.Base
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using WeebreeOpen.ErpLib.Practices;

    /// <summary>
    /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Reference/Entity_Model/CurrencyConversionRate
    /// </summary>
    public partial class CurrencyExchangeRate : BaseEntity
    {
        #region Constructors

        #endregion

        #region Key

        [Key]
        public int CurrencyExchangeRatePkId { get; set; }

        #endregion

        #region References

        public int FromCurrencyId { get; set; }

        public Currency FromCurrency { get; set; }

        public int ToCurrencyId { get; set; }

        public Currency ToCurrency { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// The Conversion Rate Type indicates the type of conversion rate.
        /// This allows you to enter multiple rates for the same currency pair.
        /// For example, one rate may be used for Spot conversions and a different rate for Revaluations.
        /// http://wiki.openbravo.com/wiki/ERP/3.0/Developers_Guide/Database_Model/org.openbravo.model.common.currency/C_Conversion_Rate#Conversion_Rate_Type
        /// </summary>
        public int ConversionRateType { get; set; }

        /// <summary>
        /// To convert Source number to Target number, the Source is multiplied by the multiply rate.
        /// If the Multiply Rate is entered, then the Divide Rate will be automatically calculated.
        /// </summary>
        public decimal MultipleRate { get; set; }

        /// <summary>
        /// To convert Source number to Target number, the Source is divided by the divide rate.
        /// If you enter a Divide Rate, the Multiply Rate will be automatically calculated.
        /// </summary>
        public decimal DevideRate { get; set; }

        public DateTimeOffset DateTimeValidFrom { get; set; }

        public DateTimeOffset DateTimeValidTo { get; set; }

        #endregion
    }
}